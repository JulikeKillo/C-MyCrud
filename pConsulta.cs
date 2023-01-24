using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSMySQL_SMART_CRUD_V1
{

    /// <summary>
    /// Clase donde se realizan las consultas CRUD
    /// </summary>
    internal class pConsulta
    {
        /// <summary>
        /// Instancias de las clases ConexionMySql y de modeloCrud
        /// </summary>

        private ConexionMySql conexionMySql;
        private List<modeloCrud> modeloCrudList;

        /// <summary>
        /// Constructor de las consultas
        /// </summary>
        public pConsulta()
        {
            conexionMySql= new ConexionMySql();
            modeloCrudList = new List<modeloCrud>();
        }
        /// <summary>
        /// Consulta que se realiza para ejecutar una query
        /// y recibira la informacion consultada.
        /// Recibe el parámetro filtro que trabaja para la query dando datos del Modelo
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<modeloCrud> consultaDatos(string filtro)
        {
            string QUERY = "SELECT * FROM cs_crud";
            MySqlDataReader mReader = null;
            modeloCrud mCrud;
            try
            {
                if (filtro != "")
                {
                    QUERY += " WHERE " +
                        "id LIKE '%" + filtro + "%' OR " +
                        "nombre LIKE '%" + filtro + "%' OR " +
                        "apellidos LIKE '%" + filtro + "%' OR " +
                        "genero LIKE '%" + filtro+"%';";
                }

                MySqlCommand mCommand = new MySqlCommand(QUERY);
                mCommand.Connection = conexionMySql.GetConnection();
                mReader = mCommand.ExecuteReader();

                

                while (mReader.Read())
                {
                    mCrud = new modeloCrud();
                    mCrud.id = mReader.GetInt32("id");
                    mCrud.nombre = mReader.GetString("nombre");
                    mCrud.apellidos = mReader.GetString("apellidos");
                    mCrud.genero = mReader.GetString("genero");
                    modeloCrudList.Add(mCrud);



                }
                mReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return modeloCrudList;
        }

        /// <summary>
        /// Método que agrega datos a la tabla 
        /// Utilizando la instanciación de modeloCrud
        /// </summary>
        /// <param name="mCrud"></param>
        /// <returns></returns>
        public bool agregaDatos (modeloCrud mCrud)
        {
            string INSERT = "INSERT INTO cs_crud(nombre,apellidos,genero)"+" values(@nombre,@apellidos,@genero);";

            MySqlCommand mCommand = new MySqlCommand(INSERT, conexionMySql.GetConnection());

            mCommand.Parameters.Add(new MySqlParameter("@nombre", mCrud.nombre));

            mCommand.Parameters.Add(new MySqlParameter("@apellidos", mCrud.apellidos));

            mCommand.Parameters.Add(new MySqlParameter("@genero", mCrud.genero));

            return mCommand.ExecuteNonQuery()>0;

            

        }
        /// <summary>
        /// Método que modifica  datos de la tabla 
        /// Utilizando la instanciación de modeloCrud
        /// </summary>
        /// <param name="mCrud"></param>
        /// <returns></returns>

        public bool modificarDatos(modeloCrud mCrud)
        {
            string UPDATE = " UPDATE cs_crud " +
                "SET nombre = @nombre, " +
                "apellidos = @apellidos, " +
                "genero = @genero " +
                "WHERE id=@id;";

            MySqlCommand mCommand = new MySqlCommand(UPDATE, conexionMySql.GetConnection());
           
            mCommand.Parameters.Add(new MySqlParameter("@nombre", mCrud.nombre));
            mCommand.Parameters.Add(new MySqlParameter("@apellidos", mCrud.apellidos));
            mCommand.Parameters.Add(new MySqlParameter("@genero", mCrud.genero));
            mCommand.Parameters.Add(new MySqlParameter("@id", mCrud.id));



            return mCommand.ExecuteNonQuery() > 0;
        }
        /// <summary>
        /// Método que elimina datos de la tabla 
        /// Utilizando la instanciación de modeloCrud
        /// </summary>
        /// <param name="mCrud"></param>
        /// <returns></returns>

        public bool eliminaDatos(modeloCrud mCrud)
        {
            string DELETE = " DELETE FROM cs_crud WHERE id=@id;";
            
        

            MySqlCommand mCommand = new MySqlCommand(DELETE, conexionMySql.GetConnection());

            mCommand.Parameters.Add(new MySqlParameter("@id", mCrud.id));



            return mCommand.ExecuteNonQuery() > 0;
        }

    }
}
