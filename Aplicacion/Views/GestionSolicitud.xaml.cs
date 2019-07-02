using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Solicitud = Aplicacion.Models.Solicitud;
using EstadoSolicitud = Aplicacion.Models.EstadoSolicitud;

namespace Aplicacion.Views
{
    /// <summary>
    /// Lógica de interacción para GestionUsuario.xaml
    /// </summary>
    public partial class GestionSolicitud : Page
    {
        public RestClient Client { get; }
        public GestionSolicitud()
        {
            InitializeComponent();
            Client = new RestClient(UrlUtils.BaseUrl);
            UpdateGrid();

            var rq = new RestRequest("estadosol", Method.GET);
            var rs = Client.Execute<List<EstadoSolicitud.Get>>(rq);

            PckPutEstado.ItemsSource = rs.Data;
        }

        public void UpdateGrid()
        {
            var rq = new RestRequest("solicitud", Method.GET);
            var rs = Client.Execute<List<Solicitud.Get>>(rq);

            if (!rs.IsSuccessful)
            {
                MessageBox.Show("No se pudieron cargar los datos");
            }
            else
            {
                GridSolicitud.ItemsSource = rs.Data;
            }
        }

        private void BtnBack_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(Object sender, RoutedEventArgs e)
        {
            var estado = PckPutEstado.SelectedItem as EstadoSolicitud.Get;
            var solicitud = GridSolicitud.SelectedItem as Solicitud.Get;
            // Validaciones
            {
                if(estado is null)
                {
                    MessageBox.Show("Seleccione un estado");
                    return;
                }

                if (solicitud is null)
                {
                    MessageBox.Show("Seleccione una fila para modificar");
                    return;
                }

                if(solicitud.Id_estado == 3)
                {
                    MessageBox.Show("No se puede modificar una solicitud finalizada");
                    return;
                }

                if(estado.Id_Estado == 3)
                {
                    var r = MessageBox.Show(
                        "Si continúa, no se podrá modificar posteriormente", 
                        "Confirmar",
                        MessageBoxButton.OKCancel);

                    if(r != MessageBoxResult.OK) { return; }
                }
            }

            var rq = new RestRequest($"solicitud/{solicitud.Id_solicitud}", Method.PUT);
            rq.AddJsonBody(new
            {
                estado = estado.Id_Estado,
                fin = estado.Id_Estado == 3 ? (DateTime?)DateTime.Now : null
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
            var s = GridSolicitud.SelectedItem as Solicitud.Get;
            // Validaciones
            {
                if (s is null)
                {
                    MessageBox.Show("Seleccione una fila");
                    return;
                }
            }

            var rq = new RestRequest($"solicitud/{s.Id_solicitud}", Method.DELETE);
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

        private void Button_Click_2(Object sender, RoutedEventArgs e)
        {
            var s = GridSolicitud.SelectedItem as Solicitud.Get;
            // Validaciones
            {
                if (s is null)
                {
                    MessageBox.Show("Seleccione una fila");
                    return;
                }
            }

            NavigationService.Navigate(new GestionInspeccion(s.Id_solicitud.ToString()));
        }
    }
}
