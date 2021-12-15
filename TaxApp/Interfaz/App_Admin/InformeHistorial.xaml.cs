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
using TaxApp.taxiBDDTableAdapters;
using static TaxApp.taxiBDD;

namespace TaxApp.Interfaz.App_Admin
{
    /// <summary>
    /// Lógica de interacción para InformeHistorial.xaml
    /// </summary>
    public partial class InformeHistorial : Window
    {
        DateTime desde,hasta;
        int num;

        public InformeHistorial()
        {
            InitializeComponent();
        }

        private void Mostrar_Click(object sender, RoutedEventArgs e)
        {
            if(Desde.SelectedDate.HasValue || Hasta.SelectedDate.HasValue)
            {
                try
                {
                    desde = Desde.SelectedDate.Value;
                    hasta = Hasta.SelectedDate.Value;

                    Viaje_TaxiTableAdapter adapter = new Viaje_TaxiTableAdapter();
                    adapter.Connection.Open();
                    Viaje_TaxiDataTable data = adapter.GetFiltroFecha(desde, hasta);
                    num = int.Parse(adapter.ContarNumViajes(desde, hasta).ToString());
                    adapter.Connection.Close();

                    DataGrid.ItemsSource = data.DefaultView;

                    MessageBox.Show("Se han realizado " + num + " viajes y se ha recaudado un total a " + total(data) + "€ en el tiempo seleccionado.");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
            else
            {
                MessageBox.Show("Falta alguna de las fechas");
            }
        }

        private void Atras_Click(object sender, RoutedEventArgs e)
        {
            aplicacion_admin window1 = new aplicacion_admin();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private double total (Viaje_TaxiDataTable data)
        {
            double res =0;
            for(int i = 0; i < data.Count; i++)
            {
                res = res+double.Parse(data.Rows[i][8].ToString());
            }
            return res;
        }
    }
}
