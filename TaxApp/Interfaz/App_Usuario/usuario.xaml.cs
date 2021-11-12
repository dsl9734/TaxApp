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
using DevExpress.Map;
using DevExpress.Xpf.Map;

namespace TaxApp.Interfaz.App_Usuario
{
    /// <summary>
    /// Lógica de interacción para usuario.xaml
    /// </summary>
    public partial class usuario : Window
    {
        private Point mousePointO;
        private Point mousePointD;

        MapItemStorage storage = new MapItemStorage();

        public usuario()
        {
            InitializeComponent();
        }

        private void E_Loaded(object sender, RoutedEventArgs e)
        {
            Mapa.ZoomLevel = 12;
            Mapa.CenterPoint = new GeoPoint(40.381841, -3.686841);

        }
        private void Posición(object sender, MouseButtonEventArgs e)
        {
            mousePointO = e.GetPosition(Mapa);
            Origen.Text = "X: " + mousePointO.X + " , Y: " + mousePointO.Y;
        }

        private void Destino(object sender, MouseButtonEventArgs e)
        {
            mousePointD = e.GetPosition(Mapa);
            Destino_T.Text = "X: " + mousePointD.X + " , Y: " + mousePointD.Y;
        }

        private String calcularTarifa(Point origen,Point destino)
        {
            double distancia = Math.Sqrt((Math.Pow(origen.X - destino.X, 2) + Math.Pow(origen.Y - destino.Y, 2)));
            double precioBase = 10;
            double precioKm = 0.1;
            double precio = Math.Round(precioBase + precioKm * distancia,2);
            if(precio >= 0)
            {
                MessageBox.Show("El precio del Viaje es de "+ precio +" €");
                return "" + precio;
            }
            else
            {
                MessageBox.Show("Error: No se ha podido calcular correctamente la tarifa.");
                return null;
            }
        }

        private void Destino_T_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Void
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            calcularTarifa(mousePointO, mousePointD);
            aplicacion_usuario window1 = new aplicacion_usuario();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            aplicacion_usuario window1 = new aplicacion_usuario();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }
    }
}
