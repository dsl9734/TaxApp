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
                try { int res = us.inicioSesion(usuario);
                    if (res == -1)
                    {
                        MessageBox.Show("Error de inicio de sesion.");
                    }
                    else
                    {
                        if (Usuario.Text == "admin")
                        {
                            aplicacion_usuario window1 = new aplicacion_usuario();
                            this.Visibility = Visibility.Hidden;
                            window1.Show();
                        }
                        else
                        {
                            aplicacion_admin window1 = new aplicacion_admin();
                            this.Visibility = Visibility.Hidden;
                            window1.Show();
                        }
                    }
                }
                catch { MessageBox.Show("Ha ocurrido un error al iniciar sesión."); }
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
