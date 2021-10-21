using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaxApp.Usuario
{
    class Usuario
    {
        string nombre;
        string correo;
        string tlf;
        string tarjeta;

        public Usuario(string nombre, string correo, string tlf, string tarjeta)
        {
            this.Nombre = nombre;
            this.Correo = correo;
            this.Tlf = tlf;
            this.Tarjeta = tarjeta;
        }

        public Usuario()
        {
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Tlf { get => tlf; set => tlf = value; }
        public string Tarjeta { get => tarjeta; set => tarjeta = value; }

        public string crearUsuario (Usuario usuario)
        {
            return "USE [Taxi] INSERT INTO[dbo].[usuario]([nombre],[correo],[tlf],[tarjeta])" +
                "VALUES('" + usuario.nombre + "','" + usuario.correo + "','" + usuario.tlf + "','" + usuario.tarjeta  + "');";
        }

        public string getUsuario (int idUsuario)
        {
            return "SELECT * FROM [Taxi].[dbo].[usuario] WHERE idUsuario = '" + idUsuario + "';";
        }

        public string getIdUsuario(string usuario)
        {
            return "SELECT idUsuario FROM [Taxi].[dbo].[usuario] WHERE nombre = '" + usuario + "';";
        }

        public string inicioSesionSQL (int idUsuario)
        {
            return "USE [Taxi] INSERT INTO[dbo].[sesion] ([Usuario_idUsuario],[fecha_hora]) VALUES (" +
                + idUsuario + "','" + DateTime.Now +");";
        }

        public int crearUsuario(string nombre, string correo, string tlf, string tarjeta)
        {
            Conexion conexion = new Conexion();

            Usuario usuario = new Usuario(nombre, correo, tlf,tarjeta);
            try
            {
                string sqlUsuario = this.crearUsuario(usuario);
                DataTable dataU = conexion.ejecutaConsultaDataTable(sqlUsuario);
                return 0;
            }

            catch
            {
                MessageBox.Show("Se ha producido un error al intentar crear el nuevo usuario");
                return -1;
            }
        }

        public int inicioSesion (string inicioSesion)
        {
            Conexion conexion = new Conexion();
            try
            {
                string query1 = this.getIdUsuario(inicioSesion);
                DataTable dataI = conexion.ejecutaConsultaDataTable(query1);
                string query = this.inicioSesionSQL(int.Parse(dataI.Rows[0][0].ToString()));

                return conexion.ejecutaConsulta(query);
            }

            catch
            {
                MessageBox.Show("Se ha producido un error al intentar crear el nuevo usuario");
                return -1;
            }
            
        }

        // Convertir Datos Sql en objetos

        public Usuario sqlUsuario(DataTable dataU)
        {
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            try
            {
                DataRow fila = dataU.Rows[0];
                usuario.nombre = fila[0].ToString();
                usuario.correo = fila[1].ToString();
                usuario.tlf = fila[2].ToString();
                usuario.tarjeta = fila[3].ToString();
                return usuario;
            }

            catch
            {
                MessageBox.Show("Se ha producido un error al recuperar los datos de la Base de Datos");
                return null;
            }
        }

    }
}
