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
using System.Windows.Shapes;

namespace TaxApp.Interfaz
{
    /// <summary>
    /// Lógica de interacción para aplicacion_usuario.xaml
    /// </summary>
    public partial class aplicacion_usuario : Window
    {
        public aplicacion_usuario()
        {
            InitializeComponent();
        }

        private void Button_Cerrar(object sender, RoutedEventArgs e)
        {
            inicio window1 = new inicio();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }
    }
}
