using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaxApp.taxiBDDTableAdapters;
using static TaxApp.taxiBDD;

namespace TaxApp.Usuario
{
    class Usuario
    {
        string nombre;
        string correo;
        string tlf;
        string tarjeta;
        string contrasena;
        Conexion con = new Conexion();

        public Usuario(string nombre, string correo, string tlf, string tarjeta, string contrasena)
        {
            this.Nombre = nombre;
            this.Correo = correo;
            this.Tlf = tlf;
            this.Tarjeta = tarjeta;
            this.contrasena = contrasena;
        }

        public Usuario()
        {
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Tlf { get => tlf; set => tlf = value; }
        public string Tarjeta { get => tarjeta; set => tarjeta = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }


        //...............................................SQL..................USUARIO..........................................
        public string crearUsuario(Usuario usuario)
        {
            return "USE [Taxi] INSERT INTO [dbo].[Usuario]([nombre],[correo],[tlf],[metodo_pago],[contrasena])" +
                "VALUES('" + usuario.nombre + "','" + usuario.correo + "','" + usuario.tlf + "','" + usuario.tarjeta + "', '" + usuario.contrasena + "')";
        }

        public string getUsuario(int idUsuario)
        {
            return "SELECT * FROM [Taxi].[dbo].[Usuario] WHERE idUsuario = '" + idUsuario + "'";
        }

        public string getIdUsuario(string usuario)
        {
            return "SELECT idUsuario FROM [taxi].[dbo].[Usuario] WHERE [taxi].[dbo].[Usuario].nombre = '" + usuario + "'";
        }

        public SqlCommand sqlGetIdUsuario(string idUsuario,string contrasena)
        {
            SqlCommand command = new SqlCommand(@"SELECT idUsuario, nombre, correo, tlf, metodo_pago, contrasena
                                                FROM     Usuario
                                                WHERE  (nombre = @nombre) AND (contrasena = @contrasena)");
            //Parametrizar
            command.Parameters.AddWithValue(@nombre, idUsuario);
            command.Parameters.AddWithValue(@contrasena, contrasena);
            return command;
        }

        public string updateUsuarioSQL(Usuario usuario, int idUsuario)
        {
            return "UPDATE [Taxi].[dbo].Usuario SET [nombre] = '" + usuario.nombre + "' ,[correo] = '" + usuario.correo + "' ,[tlf] = '" + usuario.tlf + "' ,[metodo_pago] = '" + usuario.tarjeta + ",[contrasena] = '" + usuario.contrasena + " WHERE idUsuario = '" + idUsuario + "'";
        }

        public string deleteUsuarioSQL(int id)
        {
            return "DELETE FROM [Taxi].[dbo].[Usuario] WHERE idUsuario = '" + id + "'";
        }
        // .......................................SQL.........SESION...........................................
        public string inicioSesionSQL(int idUsuario, string contrasena)
        {
            return "USE [Taxi] INSERT INTO [Taxi].[dbo].[Sesion]([Usuario_idUsuario],[fecha_hora],[contrasena])VALUES ('" +
                +idUsuario + "', '" + DateTime.Now + "', '" + contrasena + "')";
        }

        public string getSesiones()
        {
            return "SELECT * FROM [Taxi].[dbo].[Sesion] ORDER BY idSesion DESC";
        }

        public string getIdUsuarioSesion(int idSesion)
        {
            return "SELECT Usuario_idUsuario FROM [Taxi].[dbo].[Sesion] WHERE idSesion = '" + idSesion + "'";
        }


        public string deleteSesionSQL(int idSesion)
        {
            return "DELETE FROM [Taxi].[dbo].[Sesion] WHERE idSesion = '" + idSesion + "'";
        }
        //.............................................FUNCIONES DE USUARIO Y SESIONES................................
        public int crearUsuario(string nombre, string correo, string tlf, string tarjeta, string contrasena)
        {
            Conexion conexion = new Conexion();

            Usuario usuario = new Usuario(nombre, correo, tlf, tarjeta, contrasena);
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

        public int inicioSesion(string inicioSesion, string contrasena)
        {
            try
            {   
                //Comprobar inicio sesión inicio
                UsuarioTableAdapter comprobar = new UsuarioTableAdapter();
                con.connection();
                
                UsuarioDataTable sComprobar = comprobar.ComprobarSesion(inicioSesion, contrasena);
                int i = comprobar.FillBy2(sComprobar, inicioSesion, contrasena);
                con.closeConnection();
                //Comprobar inicio sesión fin
                int id = int.Parse(sComprobar.Rows[0][0].ToString());
                if (id > 0)
                {
                    // Insertar Sesion Inicio
                    SesionTableAdapter adapter = new SesionTableAdapter();
                    adapter.Connection.Open();
                    adapter.InsertSesion(id);
                    adapter.Connection.Close();
                    // Insertar Sesion Fin
                    return id;
                }
                else
                {
                    MessageBox.Show("No se ha iniciado sesión correctamente");
                    comprobar.Transaction.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


        }

        public int getSesionActual()
        {
            Conexion conexion = new Conexion();
            string sql1 = this.getSesiones();
            try
            {
                DataTable dataSesion = conexion.ejecutaConsultaDataTable(sql1);
                return int.Parse(dataSesion.Rows[0][0].ToString());
            }
            catch
            {
                MessageBox.Show("Error interno en sesion");
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
                usuario.contrasena = fila[4].ToString();
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
