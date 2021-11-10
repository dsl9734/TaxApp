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
        private CoordPoint origen;
        private CoordPoint destino;

        public usuario()
        {
            InitializeComponent();
        }

        private void Posición(object sender, MouseButtonEventArgs e)
        {
            origen = Mapa.ScreenPointToMapUnit(e.GetPosition(""));
            Origen.Text = origen.ToString();
        }

        private void Destino(object sender, MouseButtonEventArgs e)
        {
            destino = Mapa.ScreenPointToMapUnit(e.GetPosition(""));
            Destino_T.Text = destino.ToString();
        }

        private String calcularTarifa(Func<CoordPoint,MapUnit> origen,Func<CoordPoint, MapUnit> destino)
        {
            int precio = 0;
            if(precio >= 0)
            {
                return "" + precio;
            }
            else
            {
                MessageBox.Show("Error: No se ha podido calcular correctamente la tarifa.");
                return null;
            }
        }
    }
}
