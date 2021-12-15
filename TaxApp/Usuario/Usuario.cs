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

        public string deleteUsuarioSQL(int id)
        {
            return "DELETE FROM [Taxi].[dbo].[Usuario] WHERE idUsuario = '" + id + "'";
        }
       
        //.............................................FUNCIONES DE USUARIO Y SESIONES................................
        public int crearUsuario(string nombre, string correo, string tlf, string tarjeta, string contrasena)
        {
            try
            {
                UsuarioTableAdapter adapter = new UsuarioTableAdapter();
                adapter.Connection.Open();
                adapter.InsertUsuario(nombre, correo, tlf, tarjeta, contrasena);
                adapter.Connection.Close();
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
                comprobar.Connection.Open();
                
                UsuarioDataTable sComprobar = comprobar.ComprobarSesion(inicioSesion, contrasena);
                int i = comprobar.FillBy2(sComprobar, inicioSesion, contrasena);
                comprobar.Connection.Close();
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
            SesionTableAdapter sesion = new SesionTableAdapter();
            try
            {
                sesion.Connection.Open();
                SesionDataTable data = sesion.GetSesionActual();
                sesion.Connection.Close();

                return int.Parse(data.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public Boolean NombreSinUsar(string nombre)
        {
            Boolean cierto = false;
            try
            {
                UsuarioTableAdapter us = new UsuarioTableAdapter();
                us.Connection.Open();
                cierto = us.GetIdUsuario(nombre).Rows.Count == 0;
                us.Connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return cierto;
        }
        // Convertir Datos Sql en objetos

        public Usuario sqlUsuario(UsuarioDataTable dataU)
        {
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
