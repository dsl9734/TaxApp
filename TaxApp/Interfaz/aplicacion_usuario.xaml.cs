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
    /// Lógica de interacción para aplicacion_usuario.xaml
    /// </summary>
    public partial class aplicacion_usuario : Window
    {
        Conexion conexion = new Conexion();
        Usuario.Usuario usuario = new Usuario.Usuario();

        public aplicacion_usuario()
        {
            InitializeComponent();
        }

        private void Button_Cerrar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialog = MessageBox.Show("¿Seguro que deasea cerrar sesión?", "Cerrar Sesión", MessageBoxButton.YesNo);
            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    string query = usuario.deleteSesionSQL(usuario.getSesionActual());
                    conexion.ejecutaConsulta(query);

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

        private void Button_New_Viaje(object sender, RoutedEventArgs e)
        {
            App_Usuario.usuario window1 = new App_Usuario.usuario();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Cambiar_Datos(object sender, RoutedEventArgs e)
        {
            App_Usuario.modificar window1 = new App_Usuario.modificar();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Borrar_Cuenta_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialog = MessageBox.Show("¿Seguro que deasea borrar su cuenta?", "Borrar Cuenta" ,MessageBoxButton.YesNo);
            if(dialog == MessageBoxResult.Yes)
            {
                try
                {
                    int idS = usuario.getSesionActual();

                    string query = usuario.deleteSesionSQL(idS);
                    conexion.ejecutaConsulta(query);

                    string query1 = usuario.getIdUsuarioSesion(idS);
                    DataTable data2 = conexion.ejecutaConsultaDataTable(query1);
                    string query3 = usuario.deleteUsuarioSQL(int.Parse(data2.Rows[0][0].ToString()));
                    conexion.ejecutaConsulta(query3);

                    inicio window1 = new inicio();
                    this.Visibility = Visibility.Hidden;
                    window1.Show();
                }

                catch
                {
                    MessageBox.Show("No se ha podido borrar el usuario correctamente");
                }
            }
        }
    }
}
