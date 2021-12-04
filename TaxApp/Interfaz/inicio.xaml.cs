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
        string usuario,contrasena;
        Usuario.Usuario us = new TaxApp.Usuario.Usuario(); 

        public inicio()
        {
            InitializeComponent();
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            contrasena = Password.Text;
        }

        private void TextBox_Usuario(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            usuario = Usuario.Text;
        }

        private void Iniciar_Click(object sender, RoutedEventArgs e)
        {
            if (usuario != null && contrasena != null)
            {
                try { int res = us.inicioSesion(usuario,contrasena);
                    if (res == -1)
                    {
                        MessageBox.Show("Error de inicio de sesion. Usuario y/o contraseña son incorrectos.");
                    }
                    else
                    {
                        if (Usuario.Text != "admin") 
                        {
                            //Comprobar inicio sesión inicio
                            taxiBDDTableAdapters.SesionTableAdapter comprobar = new taxiBDDTableAdapters.UsuarioTableAdapter();
                            comprobar.;
                            adapter.Connection.Open();
                            adapter.Connection.BeginTransaction();
                            adapter.Connection.Close();
                            //Comprobar inicio sesión fin
                            // Insertar Sesion Inicio
                            taxiBDDTableAdapters.SesionTableAdapter adapter = new taxiBDDTableAdapters.SesionTableAdapter();
                            adapter.InsertSesion(1);
                            adapter.Connection.Open();
                            adapter.Connection.BeginTransaction();
                            adapter.Connection.Close();
                            // Insertar Sesion Fin
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
            else
            {
                MessageBox.Show("Alguna de las credenciales está vacía");
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
