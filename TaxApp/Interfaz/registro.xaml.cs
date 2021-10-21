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
    /// Lógica de interacción para registro.xaml
    /// </summary>
    public partial class registro : Window
    {
        string nombre, email, tlf, tarjeta;

        public registro()
        {
            InitializeComponent();
        }

        private void TextBox_Movil(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            nombre = Nombre.Text;
        }

        private void Button_Aceptar(object sender, RoutedEventArgs e)
        {
            aplicacion window1 = new aplicacion();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            inicio window1 = new inicio();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void TextBox_Tarjeta(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            email = Email.Text;
        }

        private void TextBox_EMail(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            tlf = TlfMovil.Text;
        }

        private void TextBox_NombreDeUsuario(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            tarjeta = NºTarjeta.Text;
        }
    }
}
