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
using System.Windows.Shapes;

namespace TaxApp.Interfaz
{
    /// <summary>
    /// Lógica de interacción para inicio.xaml
    /// </summary>
    public partial class inicio : Window
    {
        string usuario;
        Usuario.Usuario us = new TaxApp.Usuario.Usuario(); 

        public inicio()
        {
            InitializeComponent();
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Usuario(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            usuario = Usuario.Text;
        }

        private void Iniciar_Click(object sender, RoutedEventArgs e)
        {
            if (usuario != null)
            {
                us.inicioSesion(usuario);
                aplicacion window1 = new aplicacion();
                this.Visibility = Visibility.Hidden;
                window1.Show();
            }
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            registro window1 = new registro();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }
    }
}
