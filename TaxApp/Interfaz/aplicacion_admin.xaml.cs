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

namespace TaxApp.Interfaz
{
    /// <summary>
    /// Lógica de interacción para aplicacion_admin.xaml
    /// </summary>
    public partial class aplicacion_admin : Window
    {
        Conexion conexion = new Conexion();
        Usuario.Usuario usuario = new Usuario.Usuario();
        public aplicacion_admin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Conexion conexion = new Conexion();
            DataTable data = new DataTable();
            // Rellenar del Historial, preguntar

        }

        private void Button_Cerrar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialog = MessageBox.Show("¿Seguro que deasea cerrar sesión?", "Borrar Cuenta", MessageBoxButton.YesNo);
            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    conexion.ejecutaConsulta(usuario.deleteUsuarioSQL(usuario.getSesionActual()));

                    inicio window1 = new inicio();
                    this.Visibility = Visibility.Hidden;
                    window1.Show();
                }

                catch
                {
                    MessageBox.Show("No se ha podido cerrar sesión correctamente");
                }
            }
        }


        private void Button_Mis_Taxis(object sender, RoutedEventArgs e)
        {
            App_Admin.taxis window1 = new App_Admin.taxis();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Viajes(object sender, RoutedEventArgs e)
        {
            App_Admin.viajes window1 = new App_Admin.viajes();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }
    }
}
