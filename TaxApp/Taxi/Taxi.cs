using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxApp.Taxi
{
    class Taxi
    {
        string estado;
        string ubicacion;
        string destino;

        public Taxi(string estado, string ubicacion, string destino)
        {
            this.estado = estado;
            this.ubicacion = ubicacion;
            this.destino = destino;
        }

        public Taxi()
        {
        }

        public string Estado { get => estado; set => estado = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        public string Destino { get => destino; set => destino = value; }

        public string crearTaxi(Taxi taxi)
        {
            return "USE [Taxi] INSERT INTO[dbo].[taxi]([estado],[ubicacion],[destino])" +
                "VALUES('" + taxi.estado + "','" + taxi.ubicacion + "','" + taxi.destino + "','" + "');";
        }

        public string getTaxi(int idTaxi)
        {
            return "SELECT * FROM [taxi].[dbo].[usuario] WHERE idUsuario = " + idTaxi + ";";
        }

        public string getTaxis()
        {
            return "SELECT * FROM [taxi].[dbo].[taxi];";
        }

        public string setTaxi(Taxi taxi, int idTaxi)
        {
            return "USE [taxi] UPDATE [dbo].[taxi] SET [estado] = '" + taxi.estado + "',[ubicacion] = '" + taxi.ubicacion + "',[destino] = '" + taxi.destino + "' WHERE idTaxi = " + idTaxi;
        }
    }
}
