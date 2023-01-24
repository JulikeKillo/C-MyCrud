using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMySQL_SMART_CRUD_V1
{
    /// <summary>
    /// Clase Modelo
    /// </summary>
    internal class modeloCrud
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public string apellidos { get; set; }

        public string genero { get; set; }
    }
}
