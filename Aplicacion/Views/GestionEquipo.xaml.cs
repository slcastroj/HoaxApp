using RestSharp;
using System.Collections.Generic;
using System.Windows.Controls;
using Equipo = Aplicacion.Models.Equipo;
using System;
using System.Windows;

namespace Aplicacion.Views
{
    /// <summary>
    /// Lógica de interacción para GestionUsuario.xaml
    /// </summary>
    public partial class GestionEquipo : Page
    {
        public RestClient Client { get; }
        public GestionEquipo()
        {
            InitializeComponent();

            Client = new RestClient(UrlUtils.BaseUrl);
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            var rq = new RestRequest("equipo", Method.GET);
            var rs = Client.Execute<List<Equipo.Get>>(rq);

            if (!rs.IsSuccessful)
            {
                MessageBox.Show("No se pudieron cargar los datos");
            }
            else
            {
                GridEquipo.ItemsSource = rs.Data;
            }
        }

        private void Button_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            // Validaciones
            {

                if (String.IsNullOrWhiteSpace(TxtPostEncargado.Text))
                {
                    MessageBox.Show("Encargado requerido");
                    return;
                }
            }

            var rq = new RestRequest("equipo", Method.POST);
            rq.AddJsonBody(new
            {
                encargado = TxtPostEncargado.Text,
                disponible = ChkPostDisponible.IsChecked
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

        private void Button_Click_2(Object sender, RoutedEventArgs e)
        {
            var u = GridEquipo.SelectedItem as Equipo.Get;
            // Validaciones
            {
                if (u is null)
                {
                    MessageBox.Show("Seleccione algún equipo");
                    return;
                }
            }

            var rq = new RestRequest($"equipo/{u.Id_equipo}", Method.DELETE);
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

        private void Button_Click_1(Object sender, RoutedEventArgs e)
        {
            var u = GridEquipo.SelectedItem as Equipo.Get;
            // Validaciones
            {
                if (u is null)
                {
                    MessageBox.Show("Seleccione algún equipo");
                    return;
                }
            }

            var rq = new RestRequest($"equipo/{u.Id_equipo}", Method.PUT);
            rq.AddJsonBody(new
            {
                disponible = ChkPutDisponible.IsChecked
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
    }
}
