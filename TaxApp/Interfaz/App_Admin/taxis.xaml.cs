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

namespace TaxApp.Interfaz.App_Admin
{
    /// <summary>
    /// Lógica de interacción para taxis.xaml
    /// </summary>
    public partial class taxis : Window
    {
        public taxis()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Conexion conexion = new Conexion();
            Taxi.Taxi taxi = new Taxi.Taxi();
            //DataTable data = conexion.ejecutaConsultaDataTable(taxi.getTaxis());

            // Introducir datos en Data Grid
            
        }
    }
}
