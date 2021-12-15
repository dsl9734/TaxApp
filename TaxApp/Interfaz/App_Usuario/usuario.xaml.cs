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
using TaxApp.taxiBDDTableAdapters;
using static TaxApp.taxiBDD;
using System.Threading.Tasks;
using System.Device.Location;

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
            double distancia = DistanciaViajeKM(origen,destino)/1000000; // De metros a Km
            double precioBase = 5;
            double precioKm = 4.0;
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

        private double DistanciaViajeKM (Point origen, Point destino)
        {

            var d1 = origen.X * (Math.PI / 180.0);
            var num1 = origen.Y * (Math.PI / 180.0);
            var d2 = destino.X * (Math.PI / 180.0);
            var num2 = destino.Y * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        private void Destino_T_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Void
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string precio = calcularTarifa(mousePointO, mousePointD);
            MessageBoxResult dialog = MessageBox.Show("¿Desea pedir el taxi?", "Pedir Taxi", MessageBoxButton.YesNo);
            if(dialog == MessageBoxResult.Yes)
            {
                try
                {
                    TaxiTableAdapter adapter = new TaxiTableAdapter();
                    adapter.Connection.Open();
                    TaxiDataTable data = adapter.GetData();
                    adapter.Connection.Close();
                    Viaje.Viaje viaje = new Viaje.Viaje();
                    int idTaxi = viaje.idTaxiMasCercano(mousePointO,data);

                    SesionTableAdapter sAdapter = new SesionTableAdapter();
                    SesionDataTable sesion = sAdapter.GetSesionActual();
                    int idUsuario = int.Parse(sesion.Rows[0][1].ToString());

                    Viaje_TaxiTableAdapter adapterT = new Viaje_TaxiTableAdapter();
                    adapterT.Connection.Open();
                    adapterT.InsertViaje(idUsuario, idTaxi,
                        DateTime.Now, mousePointO.X.ToString(), mousePointO.Y.ToString(), mousePointD.X.ToString(),
                        mousePointD.Y.ToString(), precio);
                    adapterT.Connection.Close();

                    // Actualización de estados y coordenadas
                    adapter.Connection.Open();
                    adapter.UpdateEstado("ocupado", idTaxi);
                    // adapter.UpdateCoordenadas(mousePointD.X.ToString(), mousePointD.Y.ToString(), idTaxi);
                    adapter.Connection.Close();

                    /*
                    //Esto está para probar funcionamiento
                    TaxiTableAdapter adapterTS = new TaxiTableAdapter();
                    adapterTS.Connection.Open();
                    adapterTS.UpdateEstado("disponible", idTaxi);
                    adapterTS.UpdateCoordenadas(mousePointD.X.ToString(), mousePointD.Y.ToString(), idTaxi);
                    adapterTS.Connection.Close();*/


                    // Arreglar esta parte
                    
                    // Introducir temporizador
                    double distanciaKMTaxi = DistanciaViajeKM(mousePointO, mousePointD)/1000000;
                    // Si velocidad media = 40 km/h
                    double tiempoMIL = HorasAMilisec(distanciaKMTaxi / 40);
                    TemporizadorAsync(mousePointD, idTaxi, tiempoMIL);

                    // Itroducir envío a email

                    MessageBox.Show("Se ha pedido el taxi correctamente.");
                    aplicacion_usuario window1 = new aplicacion_usuario();
                    Visibility = Visibility.Hidden;
                    window1.Show();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Ha ocurrido un error interno al crear el viaje");
                }
                
            }
        }

        private async Task TemporizadorAsync (Point destino,int idTaxi, double tiempo)
        { 
            await Task.Delay(TimeSpan.FromMilliseconds(tiempo));
            TaxiTableAdapter adapter = new TaxiTableAdapter();
            adapter.Connection.Open();
            adapter.UpdateEstado("disponible", idTaxi);
            adapter.UpdateCoordenadas(mousePointD.X.ToString(), mousePointD.Y.ToString(), idTaxi);
            adapter.Connection.Close();
        }

        private double HorasAMilisec (double km)
        {
            return km / 50 * 3.6 * Math.Pow(10, 6);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            aplicacion_usuario window1 = new aplicacion_usuario();
            Visibility = Visibility.Hidden;
            window1.Show();
        }
    }
}
