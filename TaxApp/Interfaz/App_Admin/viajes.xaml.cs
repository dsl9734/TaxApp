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
            Viaje_TaxiTableAdapter adapter = new Viaje_TaxiTableAdapter();
            adapter.Connection.Open();
            Viaje_TaxiDataTable data = adapter.GetData();
            adapter.Connection.Close();

            

            DataTable dataTable = new DataTable();
            dataTable.Rows.Add(data.Rows);
            dataTable.Columns.Add("Nombre Usuario");

            UsuarioTableAdapter adapterU = new UsuarioTableAdapter();
            UsuarioDataTable users = new UsuarioDataTable();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                
                adapterU.Connection.Open();
                adapterU.GetDataBy3(int.Parse(data.Rows[i][1].ToString()));
                adapterU.Connection.Close();

                dataTable.Rows[i][8] = users.Rows[0][1].ToString();
            }

            DataGrid.DataContext = dataTable;
        }
    }
}
