using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaxApp.Viaje
{
    class Viaje
    {
        int idUsuario, idTaxi;
        DateTime fechaHora;
        String latitud_origen, longitud_origen, latitud_destino, longitud_destino, coste;

        public Viaje() { }

        public Viaje(int idUsuario, int idTaxi, DateTime fechaHora, string latitud_origen, string longitud_origen, string latitud_destino, string longitud_destino, string coste)
        {
            this.IdUsuario = idUsuario;
            this.IdTaxi = idTaxi;
            this.FechaHora = fechaHora;
            this.Latitud_origen = latitud_origen;
            this.Longitud_origen = longitud_origen;
            this.Latitud_destino = latitud_destino;
            this.Longitud_destino = longitud_destino;
            this.Coste = coste;
        }

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int IdTaxi { get => idTaxi; set => idTaxi = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public string Latitud_origen { get => latitud_origen; set => latitud_origen = value; }
        public string Longitud_origen { get => longitud_origen; set => longitud_origen = value; }
        public string Latitud_destino { get => latitud_destino; set => latitud_destino = value; }
        public string Longitud_destino { get => longitud_destino; set => longitud_destino = value; }
        public string Coste { get => coste; set => coste = value; }

        public int idTaxiMasCercano(Point cliente, taxiBDD.TaxiDataTable data)
        {
            int id = -1;
            double distance = 0;
            double n = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                n = Math.Sqrt((Math.Pow(cliente.X - double.Parse(data.Rows[i][2].ToString()), 2) + Math.Pow(cliente.Y - double.Parse(data.Rows[i][3].ToString()), 2)));
                if (distance == 0 && data.Rows[i][1].ToString() == "disponible" || n < distance && data.Rows[i][1].ToString() == "disponible")
                {
                    distance = n;
                    id = i;
                }
            }

            return id;
        }


    }
}
