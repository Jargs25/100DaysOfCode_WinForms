using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _100DaysOdCode_WinForms
{
    public class model_productos : conexion
    {
        private SqlCommand com;
        private SqlDataAdapter adapter;

        public model_productos()
        {
            EstablecerConexion();
        }

        public int AgregarProducto(producto oProducto)
        {
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "AGREGAR_PRODUCTO";
            com.Parameters.Add("@nombre",SqlDbType.VarChar).Value = oProducto.nombre;
            com.Parameters.Add("@cantidad", SqlDbType.Int).Value = oProducto.cantidad;
            com.Parameters.Add("@precio", SqlDbType.Decimal).Value = oProducto.precio;
            com.Parameters.Add("@rutaImagen", SqlDbType.VarChar).Value = oProducto.rutaImagen;

            com.Connection = con;
            con.Open();
            int resultado = com.ExecuteNonQuery();
            con.Close();

            return resultado;
        }

        public int ModificarProducto(producto oProducto)
        {
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "MODIFICAR_PRODUCTO";
            com.Parameters.Add("@id", SqlDbType.Int).Value = oProducto.id;
            com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre;
            com.Parameters.Add("@cantidad", SqlDbType.Int).Value = oProducto.cantidad;
            com.Parameters.Add("@precio", SqlDbType.Decimal).Value = oProducto.precio;
            com.Parameters.Add("@rutaImagen", SqlDbType.VarChar).Value = oProducto.rutaImagen;

            com.Connection = con;
            con.Open();
            int resultado = com.ExecuteNonQuery();
            con.Close();

            return resultado;
        }
        public int EliminarProducto(int id)
        {
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "ELIMINAR_PRODUCTO";
            com.Parameters.Add("@id", SqlDbType.Int).Value = id;
            com.Connection = con;
            con.Open();
            int resultado = com.ExecuteNonQuery();
            con.Close();

            return resultado;
        }
        public DataTable BuscarProductos(producto oProducto)
        {
            DataTable dt = new DataTable();

            adapter = new SqlDataAdapter();
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "BUSCAR_PRODUCTOS";
            com.Parameters.Add("@codigo", SqlDbType.VarChar).Value = oProducto.codigo;
            com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre;
            com.Parameters.Add("@cantidad", SqlDbType.VarChar).Value = oProducto.cantidad < 1 ? "" : oProducto.cantidad.ToString();
            com.Parameters.Add("@precio", SqlDbType.VarChar).Value = oProducto.precio < 1 ? "" : oProducto.precio.ToString();
            com.Connection = con;
            con.Open();
            adapter.SelectCommand = com;
            adapter.Fill(dt);
            con.Close();

            return dt;
        }

    }
}
