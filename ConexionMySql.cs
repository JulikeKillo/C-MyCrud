using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySqlConnector;


namespace CSMySQL_SMART_CRUD_V1
{
    /// <summary>
    /// Clase referencia a la conexion MYSQL
    /// </summary>
    internal class ConexionMySql : conexion1
    {
        private MySqlConnection connection;

        private string cadenaConexion;

        public ConexionMySql(){
            cadenaConexion = "Database=" + database + "; DataSource="
                + server + "; User Id =" + user + "; Password =" + pwd;
                
            connection= new MySqlConnection(cadenaConexion);
        }
        /// <summary>
        /// Método propio de la clase MySqlConnection
        /// para abrir la conexion y posteriormente cerrarla
        /// </summary>
        /// <returns></returns>

        public MySqlConnection GetConnection()
        {
            try
            {
                if(connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                    MessageBox.Show("CONEXION CORRECTA");
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return connection;
        }
        public void closeConexion()
        {
            connection.Close();
        }


    }
}
