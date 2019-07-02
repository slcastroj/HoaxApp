using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Servicio = Aplicacion.Models.Servicio;

namespace Aplicacion.Views
{
    /// <summary>
    /// Lógica de interacción para GestionUsuario.xaml
    /// </summary>
    public partial class GestionServicio : Page
    {
        public RestClient Client { get; }
        public GestionServicio()
        {
            InitializeComponent();
            Client = new RestClient(UrlUtils.BaseUrl);
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            var rq = new RestRequest("servicio", Method.GET);
            var rs = Client.Execute<List<Servicio.Get>>(rq);

            if (!rs.IsSuccessful)
            {
                MessageBox.Show("No se pudieron cargar los datos");
            }
            else
            {
                GridServicio.ItemsSource = rs.Data;
            }
        }

        private void BtnBack_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(Object sender, RoutedEventArgs e)
        {
            var nombre = TxtPostNombre.Text;
            var descripcion = TxtPostDescripcion.Text;
            var costo = TxtPostCosto.Text;
            var costoint = -1;
            {
                if(String.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Ingrese nombre");
                    return;
                }

                if (String.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Ingrese descripcion");
                    return;
                }

                if (String.IsNullOrWhiteSpace(costo))
                {
                    MessageBox.Show("Ingrese costo");
                    return;
                }

                if (!Int32.TryParse(costo, out costoint) || costoint < 0)
                {
                    MessageBox.Show("Monto debe ser un número positivo");
                    return;
                }
            }

            var rq = new RestRequest("servicio", Method.POST)
                .AddJsonBody(new
                {
                    nombre,
                    descripcion,
                    costo
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
            // Validaciones
            var servicio = GridServicio.SelectedItem as Servicio.Get;
            var descripcion = TxtPutDescripcion.Text;
            var costo = TxtPutCosto.Text;
            {
                if (servicio is null)
                {
                    MessageBox.Show("Seleccione una fila");
                    return;
                }

                if (String.IsNullOrWhiteSpace(descripcion) && String.IsNullOrWhiteSpace(costo))
                {
                    MessageBox.Show("Ingrese algún campo a modificar");
                    return;
                }

                if (String.IsNullOrWhiteSpace(costo))
                {
                    costo = servicio.Costo.ToString();
                }

                if (String.IsNullOrWhiteSpace(descripcion))
                {
                    descripcion = servicio.Descripcion;
                }
            }

            var rq = new RestRequest($"servicio/{servicio.Id_servicio}", Method.PUT)
                .AddJsonBody(new
                {
                    costo,
                    descripcion
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
            var s = GridServicio.SelectedItem as Servicio.Get;
            // Validaciones
            {
                if (s is null)
                {
                    MessageBox.Show("Seleccione una fila");
                    return;
                }
            }

            var rq = new RestRequest($"servicio/{s.Id_servicio}", Method.DELETE);
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
