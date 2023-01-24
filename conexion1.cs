using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace CSMySQL_SMART_CRUD_V1
{
    /// <summary>
    /// Clase conexión que sirve para añadir
    /// la ruta de conexión a la bd
    /// </summary>
    internal class conexion1
    {
        protected string server = "localhost";
        protected string user = "root";
        protected string pwd = "root";
        protected string database = "cs_smart_crud";
    }
}
