using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static CSMySQL_SMART_CRUD_V1.pConsulta;

namespace CSMySQL_SMART_CRUD_V1
{
    /// <summary>
    /// Código de la ventana principal
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instancias de las clases ConexionMySql, modeloCrud y pConsulta
        /// </summary>

        private List<modeloCrud> modeloCrudList;
        private pConsulta mConsulta;
        private modeloCrud mcrud;

        /// <summary>
        /// Métodp de inicialización de la ventana
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
            modeloCrudList= new List<modeloCrud>();
            mConsulta= new pConsulta();
            mcrud = new modeloCrud();
            cargaDatosP();
  

        }
        /// <summary>
        /// Método que carga los datos de los textfield
        /// </summary>
        private void cargaDatos()
        {
            mcrud.id=int.Parse(idTXT.Text.Trim());
            mcrud.nombre = txfNombre.Text.Trim();
            mcrud.apellidos = txfApellidos.Text.Trim();
            mcrud.genero = cbGenero.Text.Trim();

        }
        /// <summary>
        /// Método que añade datos a la tabla desde la bd
        /// </summary>

        private void cargaDatosP(string filtro = "")
        {
            DataGrid.Items.Clear();
            DataGrid.Items.Refresh();
            modeloCrudList.Clear();
            modeloCrudList = mConsulta.consultaDatos(filtro);

            for (int i = 0; i < modeloCrudList.Count() ; i++)
            {
                DataGrid.Items.Add(modeloCrudList[i]);
              
            }
        }
        /// <summary>
        /// Método que comprueba los datos
        /// </summary>

        private bool datosCorrectos()
        {
            if (txfNombre.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el nombre del producto");
                return false;
            }

            if (txfApellidos.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el nombre del producto");
                return false;
            }


            if (cbGenero.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el nombre del producto");
                return false;
            }


            return true;
        }
        /// <summary>
        /// Método que busca datos en la tabla
        /// </summary>

        private void buscaDato(object sender, TextChangedEventArgs e)
        {
            cargaDatosP(txtBuscar.Text.Trim());
            

        }


        /// <summary>
        /// Botón que añade datos a la bd
        /// </summary>

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (!datosCorrectos())
            {
                return;
            }
            cargaDatos();

            if (mConsulta.agregaDatos(mcrud))
            {
                MessageBox.Show("Persona Agregada");
                cargaDatosP();
                LimpiaTxf();
            }
          
        }
        /// <summary>
        /// Método que elimina los datos de los Textfields
        /// </summary>
        private void LimpiaTxf()
        {
            txfNombre.Text = "";
            txfApellidos.Text = "";
            txtBuscar.Text = "";
            idTXT.Text = "";
        }
        /// <summary>
        /// Botón que modifica datos a la bd
        /// </summary>
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if(!datosCorrectos())
            
                return;
             cargaDatos();

            if (mConsulta.modificarDatos(mcrud))
            {
                MessageBox.Show("Producto modificado");
                cargaDatosP();
                LimpiaTxf();
            }
            else
                MessageBox.Show("Error al modificar el producto");
        }
        /// <summary>
        /// Botón que elimina datos a la bd
        /// </summary>
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            if (mcrud.id == -1)
            {
                return;
            }
            cargaDatos();
            if (mConsulta.eliminaDatos(mcrud))
            {
                MessageBox.Show("Dato Eliminado");
                cargaDatosP();
                LimpiaTxf();
            }

        }





        /// <summary>
        /// Método que al dar doble click sobre un campo,
        /// reflejará los datos en los TextFields para su
        /// modificación o eliminación
        /// </summary>
        private void datosClick(object sender, MouseButtonEventArgs e)
        { 
            var row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromItem(DataGrid.SelectedItem);
            var producto = (modeloCrud)row.Item;

            idTXT.Text = producto.id.ToString();
            txfNombre.Text = producto.nombre;
            txfApellidos.Text=producto.apellidos;
            cbGenero.Text = producto.genero;
        }
    }
}
