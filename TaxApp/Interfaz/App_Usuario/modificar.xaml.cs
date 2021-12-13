using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaxApp.taxiBDDTableAdapters;
using static TaxApp.taxiBDD;

namespace TaxApp.Interfaz.App_Usuario
{
    /// <summary>
    /// Lógica de interacción para modificar.xaml
    /// </summary>
    public partial class modificar : Window
    {
        string nombre, email, tlf, tarjeta, contrasena;
        Usuario.Usuario usuario = new Usuario.Usuario();
        Conexion conexion = new Conexion();

        public modificar()
        {
            InitializeComponent();
        }
        private void E_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Cargar Datos
                int id = usuario.getSesionActual();
                UsuarioTableAdapter adapter = new UsuarioTableAdapter();
                adapter.Connection.Open();
                UsuarioDataTable data = adapter.GetDataBy3(id);
                adapter.Connection.Close();
                usuario = usuario.sqlUsuario(data);
                Nombre.Text = usuario.Nombre;
                Contrasena.Text = usuario.Contrasena;
                Email.Text = usuario.Correo;
                TlfMovil.Text = usuario.Tlf;
                NºTarjeta.Text = usuario.Tarjeta;
                MessageBox.Show("Cambia cualquier dato y haz click en Guardar.");
            }
            catch
            {
                MessageBox.Show("Error al cargar los datos del usuario;");
            }           
        }
        private void TextBox_Movil(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if(TlfMovil.Text.Length == 9 && IsNumeric(TlfMovil.Text))
            tlf = TlfMovil.Text;
            else
            {
                MessageBox.Show("El número de teléfono no tiene 9 números.");
            }
        }

        private void Button_Aceptar(object sender, RoutedEventArgs e)
        {
            if (nombre == "admin")
            {
                MessageBox.Show("No se puede crear un usuario administrador");
            }
            else if (tlf.Length != 9 && IsNumeric(tlf))
            {
                MessageBox.Show("El número de teléfono no tiene 9 números.");
            }
            else if (!email_bien_escrito(email))
            {
                MessageBox.Show("Por favor introduce un email válido.");
            }
            else if (NºTarjeta.Text.Length != 16 || !IsNumeric(NºTarjeta.Text))
            {
                MessageBox.Show("Por favor introduce un número de tarjeta correcto de 12 números.");
            }
            else if (!usuario.NombreSinUsar(nombre))
            {
                MessageBox.Show("Nombre de usuario ya está en uso.");
            }
            else
            {
                // Generar UPDATE
                Usuario.Usuario usuario = new Usuario.Usuario(nombre, email, tlf, tarjeta, contrasena);
                try
                {
                    int idS = usuario.getSesionActual();

                    UsuarioTableAdapter user = new UsuarioTableAdapter();
                    user.Connection.Open();
                    int num = user.UpdateUsuario(nombre, email, tlf, tarjeta, contrasena, idS);
                    user.Connection.Close();

                    MessageBox.Show("Se han modificado los datos correctamente.");
                    aplicacion_usuario window1 = new aplicacion_usuario();
                    Visibility = Visibility.Hidden;
                    window1.Show();
                }

                catch
                {
                    MessageBox.Show("Ha ocurrido un error al modificar los datos del usuario.");
                }
            }
        }

        private void TextBox_Contrasena(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            contrasena = Contrasena.Text;
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            aplicacion_usuario window1 = new aplicacion_usuario();
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
        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsNumeric(string text)
        {
            double _out;
            return double.TryParse(text, out _out);
        }
    }
}
