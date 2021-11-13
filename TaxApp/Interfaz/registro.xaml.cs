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
            tlf = TlfMovil.Text;
        }

        private void Button_Aceptar(object sender, RoutedEventArgs e)
        {
            // Comprobaciones de datos
            if (nombre == "admin")
            {
                MessageBox.Show("No se puede crear un usuario administrador");
            }
            else if (tlf.Length != 9)
            {
                MessageBox.Show("El número de teléfono no tiene 9 números");
            }
            else
            {
                Usuario.Usuario usuario = new Usuario.Usuario(nombre,email,tlf,tarjeta,Contrasena.Text);
                Conexion conexion = new Conexion();
                // Regitro en SQL
                try
                {
                    string query = usuario.crearUsuario(usuario);
                    conexion.ejecutaConsulta(query);
                    // Inicio Sesión
                    try
                    {
                        int res = usuario.inicioSesion(nombre,Contrasena.Text);
                        if (res == -1)
                        {
                            MessageBox.Show("Error de inicio de sesion.");
                        }
                        else
                        {
                            MessageBox.Show("Usuario creado correctamente. Bienvenid@.");
                            aplicacion_usuario window1 = new aplicacion_usuario();
                            this.Visibility = Visibility.Hidden;
                            window1.Show();
                        }
                    }
                    catch { MessageBox.Show("Error al iniciar sesión con el nuevo usuario"); }
                }
                catch { MessageBox.Show("Ha ocurrido un error al crear el usuario"); }
            } 
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
            tarjeta = NºTarjeta.Text;
        }

        private void TextBox_EMail(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            email = Email.Text;
        }

        private void TextBox_NombreDeUsuario(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            nombre = Nombre.Text;
        }
    }
}
