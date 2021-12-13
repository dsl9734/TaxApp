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
using TaxApp.taxiBDDTableAdapters;
using static TaxApp.taxiBDD;

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
            // Introducir datos en Data Grid
            Conexion conexion = new Conexion();
            TaxiTableAdapter adapter = new TaxiTableAdapter();
            adapter.Connection.Open();
            TaxiDataTable data = adapter.GetData();
            adapter.Connection.Close();

            DataGrid.DataContext = data;  
        }

        private void Atrás_Click(object sender, RoutedEventArgs e)
        {
            aplicacion_admin window1 = new aplicacion_admin();
            Visibility = Visibility.Hidden;
            window1.Show();
        }
    }
}
