using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaxApp.Taxi
{
    class Taxi
    {
        string estado, latitud, longitud;

        public Taxi()
        {
        }

        public Taxi(string estado, string latitud, string longitud)
        {
            this.Estado = estado;
            this.Latitud = latitud;
            this.Longitud = longitud;
        }

        public string Estado { get => estado; set => estado = value; }
        public string Latitud { get => latitud; set => latitud = value; }
        public string Longitud { get => longitud; set => longitud = value; }

        public void CambiarEstado (string estado, int id)
        {
            try
            {
                taxiBDDTableAdapters.TaxiTableAdapter adapter = new taxiBDDTableAdapters.TaxiTableAdapter();
                adapter.Connection.Open();
                adapter.UpdateEstado(estado, id);
                adapter.Connection.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CambiarCoordenadas (Point nuevo, int id)
        {
            try
            {
                taxiBDDTableAdapters.TaxiTableAdapter adapter = new taxiBDDTableAdapters.TaxiTableAdapter();
                adapter.Connection.Open();
                adapter.UpdateCoordenadas(nuevo.X.ToString(),nuevo.Y.ToString(), id);
                adapter.Connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
