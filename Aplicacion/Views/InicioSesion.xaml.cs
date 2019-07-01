using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Usuario = Aplicacion.Models.Usuario;

namespace Aplicacion.Views
{
    /// <summary>
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : Page
    {
        public RestClient Client { get; }
        public InicioSesion()
        {
            InitializeComponent();
            Client = new RestClient(UrlUtils.BaseUrl);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rut = txtRut.Text;
            var clave = txtContrasena.Password;

            // Validaciones
            {
                if(String.IsNullOrWhiteSpace(rut))
                {
                    MessageBox.Show("Rut requerido");
                    return;
                }

                if (String.IsNullOrWhiteSpace(clave))
                {
                    MessageBox.Show("Clave requerida");
                    return;
                }
            }

            var rq = new RestRequest($"usuario/{rut}", Method.GET);
            var rs = Client.Execute<Usuario.GetSingle>(rq);

            if (!rs.IsSuccessful)
            {
                if (rs.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Rut no válido");
                    return;
                }
                else
                {
                    MessageBox.Show($"Error {rs.StatusCode}");
                    return;
                }
            }

            var u = rs.Data;

            if(u.IdTipo != 2)
            {
                MessageBox.Show("Usuario no administrador");
                return;
            }

            if(txtContrasena.Password != u.Clave)
            {
                MessageBox.Show("Contraseña incorrecta");
                return;
            }

            NavigationService.Navigate(new MenuPrincipal());
        }
    }
}
