using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Microsoft.Data.SqlClient;

using System.Windows.Forms;
using System.Drawing;

namespace Lab1CoreDavidAlvarado
{
    class Conexion
    {
        private SqlConnection conexion = new SqlConnection("Data Source=tcp:163.178.107.10;Initial Catalog=B60315;User ID=laboratorios;Password=Saucr.118");
        
        public List<string> Show_table(string tableName)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("SELECT * FROM " + tableName, conexion);
            SqlDataReader reader;
            reader = comando.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read() == true)
            {
                lista.Add(reader[0].ToString() +" | "+ reader[1].ToString() + " | " + reader[2].ToString());
            }
            conexion.Close();
            return lista;


        }

        public bool Agregar(string nombreTabla, string cod, string nombre, string div)
        {
                        
            conexion.Open();
            SqlCommand comando;
            if (nombreTabla.Equals("PAIS"))
            {
                comando = new SqlCommand(
                "INSERT INTO " + nombreTabla + " VALUES ('" + cod + "','"+nombre+"','"+div+"')", conexion);
            }
            else
            {
                comando = new SqlCommand(
                "INSERT INTO " + nombreTabla + " VALUES ('" + cod + "','" + nombre + "'," + div + ")", conexion);
            }
            try
            {
                int filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
                if (filasAfectadas > 0) return true;
                else return false;
            }
            catch(SqlException)
            {
                conexion.Close();
                return false;
            }
            
            
            
        }
        public bool Eliminar(string cod, string nombreTabla)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand(
                "DELETE FROM " + nombreTabla + " WHERE codigo = '" + cod + "'", conexion);
            int filasafectadas = cmd.ExecuteNonQuery();
            conexion.Close();
            if (filasafectadas > 0) return true;
            else return false;
            
        }


        public bool Actualizar(string cod, string nombre, string div, string tabla)
        {
            conexion.Open();
            SqlCommand cmd;
            if (tabla.Equals("PAIS"))
            {
                cmd = new SqlCommand(
                    "UPDATE PAIS SET nombre = '"+ nombre +"', divisa = '"+div+"' WHERE codigo LIKE '"+cod+"'", conexion);
            }
            else
            {
                cmd = new SqlCommand(
                    "UPDATE DIVISA SET nombre = '"+nombre+"', valorAlDolar = "+div+" WHERE codigo LIKE '"+cod+"'", conexion);
            }
            try
            {
                int filasafectadas = cmd.ExecuteNonQuery();
                conexion.Close();
                if (filasafectadas > 0) return true;
                else return false;
            }
            catch (SqlException)
            {
                conexion.Close();
                return false;
            }
            
        }

        public string Buscar(string tableName, string nombre)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("SELECT * FROM " + tableName +" WHERE nombre = '"+nombre+"'", conexion);
            SqlDataReader reader;
            reader = comando.ExecuteReader();
            string resultado = "";
            while (reader.Read() == true)
            {
                resultado = reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString();
            }
            conexion.Close();
            return resultado;


        }
        public string BuscarConvertir(string TableName, string cod)
        {
            conexion.Open();
            SqlDataReader reader;
            string resultado = "";
            SqlCommand comando;
     
            
            comando = new SqlCommand("SELECT * FROM " + TableName + " WHERE codigo = '" + cod + "'", conexion);
            reader = comando.ExecuteReader();
            while (reader.Read() == true)
            {
                resultado = reader[2].ToString();
            }
            conexion.Close();
            return resultado;
            

        }
        public bool BuscarPruebas(string TableName, string condicion, string valor)
        {
            conexion.Open();
            SqlCommand comando;
            SqlDataReader reader;
            string resultado = "";

            comando = new SqlCommand("SELECT * FROM " + TableName + " WHERE "+ condicion +" = " + valor, conexion);
            reader = comando.ExecuteReader();
            while (reader.Read() == true)
            {
                resultado = reader[2].ToString();
            }
            conexion.Close();
            if (String.IsNullOrEmpty(resultado))
            {
                return false;
            }
            else return true;


        }


        public List<string> Llenar_combo(string tableName)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand("SELECT * FROM " + tableName, conexion);
            SqlDataReader reader;
            reader = comando.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read() == true)
            {
                lista.Add(reader[0].ToString());
            }
            conexion.Close();
            return lista;


        }

        public string Convertir(string cod1, string cod2, string cantidad)
        {
            string resultado = "";

 
            //Primeo buscar para comprobar que están
            string codDiv1 = BuscarConvertir("PAIS", cod1);
            string codDiv2 = BuscarConvertir("PAIS", cod2);

            string valDiv1 = BuscarConvertir("DIVISA", codDiv1);
            string valDiv2 = BuscarConvertir("DIVISA", codDiv2);

            float div1 = float.Parse(valDiv1);
            float div2 = float.Parse(valDiv2);

            float cant = float.Parse(cantidad);

            float conversion = (div1 * cant) / div2;
            double costo = conversion * (0.02);
            double conCosto = conversion + costo;

            resultado = Math.Round(conversion).ToString() + "," + Math.Round(conCosto).ToString() + "," + Math.Round(conversion, 5).ToString() + "," + Math.Round(conCosto, 5).ToString();

            return resultado;
        }
    }
}
