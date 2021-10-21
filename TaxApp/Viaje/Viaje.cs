using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxApp.Viaje
{
    class Viaje
    {
        int idUsuario, idTaxi;
        DateTime fechaHora;
        String origen, destino, coste;

        public Viaje() { }

        public Viaje(int idUsuario, int idTaxi, DateTime fechaHora, String origen,String destino, String coste)
        {
            this.idUsuario = idUsuario;
            this.idTaxi = idTaxi;
            this.fechaHora = fechaHora;
            this.origen = origen;
            this.destino = destino;
            this.coste = coste;
        }

        //...................................................SQL..........................................................

        public String getViajesUsuarioSQL(int idUsuario)
        {
            return "SELECT * FROM [taxi].[dbo].[viaje_taxi] WHERE idUsuario = " + "'"+ idUsuario +"';";
        }

        // Date es Date.Now
        public String postViajeSQL (int idUsuario, int idTaxi, String origen,String destino, String coste)
        {
            return "INSERT INTO [dbo].[Viaje_Taxi]([Usuario_idUsuario],[Taxi_idTaxi],[fecha_hora],[origen],[destino],[coste])" + 
            + "VALUES ('"+idUsuario+"','"+idTaxi+"','"+DateTime.Now+"','"+origen+"','"+destino+"','"+coste+"')";
        }
    }
}
