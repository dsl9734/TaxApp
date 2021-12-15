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
    /// Lógica de interacción para viajes.xaml
    /// </summary>
    public partial class viajes : Window
    {
        public viajes()
        {
            InitializeComponent();
        }

        private void Ver_Viajes_Click(object sender, RoutedEventArgs e)
        {

            // introducir try
            try
            {
                Viaje_TaxiTableAdapter adapter = new Viaje_TaxiTableAdapter();
                adapter.Connection.Open();
                DataTable dataTable = adapter.GetData();
                adapter.Connection.Close();

                dataTable.Columns.Add("Nombre Usuario");

                UsuarioTableAdapter adapterU = new UsuarioTableAdapter();
                UsuarioDataTable users = new UsuarioDataTable();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {

                    adapterU.Connection.Open();
                    users = adapterU.GetDataBy3(int.Parse(dataTable.Rows[i][1].ToString()));
                    adapterU.Connection.Close();

                    dataTable.Rows[i][9] = users.Rows[0][1].ToString();
                    users.Dispose();
                }

                DataGrid.ItemsSource = dataTable.DefaultView;
                
                
            }

            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
            
        }
    }
}
