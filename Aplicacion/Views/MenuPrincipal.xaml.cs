using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Aplicacion.Views
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Page
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GestionUsuario());
        }

        private void btnSolicitud_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GestionSolicitud());
        }

        private void btnInspeccion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GestionInspeccion());
        }

        private void BtnServicio_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GestionServicio());
        }

        private void BtnBack_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnEquipo_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GestionEquipo());
        }
    }
}
