using System;
using System.Collections.Generic;
using System.Data;
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
                string query = usuario.getUsuario(id);
                DataTable data = conexion.ejecutaConsultaDataTable(query);
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
            nombre = Nombre.Text;
        }

        private void Button_Aceptar(object sender, RoutedEventArgs e)
        {
            // Generar UPDATE
            Usuario.Usuario usuario = new Usuario.Usuario(nombre,email,tlf,tarjeta,contrasena);
            try
            {
                string sql2 = usuario.updateUsuarioSQL(usuario, usuario.getSesionActual());
                conexion.ejecutaConsulta(sql2);
                MessageBox.Show("Se han modificado los datos correctamente.");
                aplicacion_usuario window1 = new aplicacion_usuario();
                this.Visibility = Visibility.Hidden;
                window1.Show();
            }

            catch
            {
                MessageBox.Show("Ha ocurrido un error al modificar los datos del usuario.");
            }
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
