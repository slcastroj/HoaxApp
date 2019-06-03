﻿using System;
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
    }
}
