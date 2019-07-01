using RestSharp;
using System.Collections.Generic;
using System.Windows.Controls;
using Usuario = Aplicacion.Models.Usuario;
using TipoUsuario = Aplicacion.Models.TipoUsuario;
using System;
using System.Windows;

namespace Aplicacion.Views
{
    /// <summary>
    /// Lógica de interacción para GestionUsuario.xaml
    /// </summary>
    public partial class GestionUsuario : Page
    {
        public RestClient Client { get; }
        public GestionUsuario()
        {
            InitializeComponent();

            Client = new RestClient(UrlUtils.BaseUrl);
            UpdateGrid();

            var rq = new RestRequest("tipousuario", Method.GET);
            var rs = Client.Execute<List<TipoUsuario.Get>>(rq);

            PckPostTipo.ItemsSource = rs.Data;
        }

        public void UpdateGrid()
        {
            var rq = new RestRequest("usuario", Method.GET);
            var rs = Client.Execute<List<Usuario.Get>>(rq);

            GridUsuario.ItemsSource = rs.Data;
        }

        private void Button_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            // Validaciones
            {
                if (String.IsNullOrWhiteSpace(TxtPostRut.Text))
                {
                    MessageBox.Show("Rut requerido");
                    return;
                }

                if (String.IsNullOrWhiteSpace(TxtPostNombre.Text))
                {
                    MessageBox.Show("Nombre requerido");
                    return;
                }

                if (String.IsNullOrWhiteSpace(TxtPostClave.Password))
                {
                    MessageBox.Show("Contraseña requerida");
                    return;
                }

                if (String.IsNullOrWhiteSpace(TxtPostEmail.Text))
                {
                    MessageBox.Show("Email requerido");
                    return;
                }

                if (PckPostFecNac.SelectedDate is null)
                {
                    MessageBox.Show("Fecha nacimiento requerida");
                    return;
                }

                if (PckPostTipo.SelectedItem is null)
                {
                    MessageBox.Show("Tipo requerido");
                    return;
                }
            }

            var rq = new RestRequest("usuario", Method.POST);
            rq.AddJsonBody(new
            {
                Rut = TxtPostRut.Text,
                Nombre = TxtPostNombre.Text,
                Clave = TxtPostClave.Password,
                Email = TxtPostEmail.Text,
                Fecha_Nac = PckPostFecNac.SelectedDate,
                ((TipoUsuario.Get)PckPostTipo.SelectedItem).Id_Tipo
            });
            var rs = Client.Execute(rq);
            if (rs.IsSuccessful)
            {
                MessageBox.Show("Registro actualizado");
                UpdateGrid();
            }
            else
            {
                MessageBox.Show($"Error {rs.StatusCode}");
            }
        }

        private void Button_Click_1(Object sender, RoutedEventArgs e)
        {
            String email = TxtPutEmail.Text;
            var u = GridUsuario.SelectedItem as Usuario.Get;
            // Validaciones
            {
                if(u is null)
                {
                    MessageBox.Show("Seleccione algún usuario");
                    return;
                }

                if (String.IsNullOrWhiteSpace(TxtPutClave.Password) &&
                    String.IsNullOrWhiteSpace(TxtPutEmail.Text))
                {
                    MessageBox.Show("Modifique algún campo");
                    return;
                }

                if (String.IsNullOrWhiteSpace(TxtPutClave.Password))
                {
                    MessageBox.Show("Contraseña requerida");
                    return;
                }

                if (String.IsNullOrWhiteSpace(email))
                {
                    email = u.Email;
                }
            }

            var rq = new RestRequest($"usuario/{u.Rut}", Method.PUT);
            rq.AddJsonBody(new
            {
                clave = TxtPutClave.Password,
                email = email,
            });
            var rs = Client.Execute(rq);

            if(rs.IsSuccessful)
            {
                MessageBox.Show("Registro actualizado");
                UpdateGrid();
            }
            else
            {
                MessageBox.Show($"Error {rs.StatusCode}");
            }
        }

        private void Button_Click_2(Object sender, RoutedEventArgs e)
        {
            var u = GridUsuario.SelectedItem as Usuario.Get;
            // Validaciones
            {
                if (u is null)
                {
                    MessageBox.Show("Seleccione algún usuario");
                    return;
                }
            }

            var rq = new RestRequest($"usuario/{u.Rut}", Method.DELETE);
            var rs = Client.Execute(rq);

            if (rs.IsSuccessful)
            {
                MessageBox.Show("Registro actualizado");
                UpdateGrid();
            }
            else
            {
                MessageBox.Show($"Error {rs.StatusCode}");
            }
        }

        private void BtnBack_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
