using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace TaxApp
{
    class Conexion
    {
        //public String connS = "server=localhost:3306;user id=root;database=taxi,password=admin";
        public String connS = "Data Source=LAPTOP-3JQ7F1RA;Integrated Security=True"; // Portatil HP
        //public String connS = "Data Source=DESKTOP-4EUEAU1;Initial Catalog=taxi;Integrated Security=True"; // Torre Casa
        public SqlConnection con;
        public Conexion()
        {
            con = new SqlConnection(connS);
        }

        //String con los datos sobre la conexión

        //DataBase Conection
        public void connection()
        {
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The system failed to establish a connection." + Environment.NewLine +
                    "Descriptions: " + ex.Message.ToString(), "C# WPF Connect to SQL Server", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Cierra la conexión con la base de datos
        public void closeConnection()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {
                //
            }
        }

        public DataTable ejecutaConsultaDataTable(String consulta)

        {
            DataTable dt = new DataTable();
            try

            {
                using (SqlCommand c = new SqlCommand(consulta, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(c);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    con.Close();
                    da.Dispose();
                }

                        return dt;

            }

            catch(SqlException ex) //(Exception ex) //Tratamiento de errores en la conexión

            {
                this.closeConnection(); //Cierro conexión

                return null;

            }
        }

        public int ejecutaConsulta(String consulta)

        {
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand c = new SqlCommand(consulta, con); //Defino la consulta a realizar.
            try

            {
                this.connection();//Abro la conexión
                c.ExecuteNonQuery();

                da.SelectCommand = c;

                this.closeConnection();

                return 0;

            }

            catch //(Exception ex) //Tratamiento de errores en la conexión

            {

                this.closeConnection(); //Cierro conexión

                return -1;

            }
        }
    }
}