using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinPuntoVenta.Entidades;

namespace WinPuntoVenta.Datos
{
    internal class D_Punto_Venta
    {
        public DataTable Listado_pv(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = new SqlConnection(Conexion.cadena);
                SqlCommand comando = new SqlCommand("USP_Listado_pv", SqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("cTexto", SqlDbType.VarChar).Value = cTexto;
                SqlCon.Open();
                Resultado = comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if(SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public string Guardar_pv(int nOpcion, E_Punto_Venta oPropiedad)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = new SqlConnection(Conexion.cadena);
                SqlCommand comando = new SqlCommand("USP_Guardar_pv", SqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("cOpcion", SqlDbType.Int).Value=nOpcion;
                comando.Parameters.Add("nId", SqlDbType.Int).Value = oPropiedad.id_pv;
                comando.Parameters.Add("cDescripcion", SqlDbType.VarChar).Value = oPropiedad.descripcion_pv;
                SqlCon.Open();
                Respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo ingresar el registro";
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
                
            }
            return Respuesta;
        }

        public string Eliminar_pv(int nId)
        {
            string respuesta;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = new SqlConnection(Conexion.cadena);
                SqlCommand comando = new SqlCommand("USP_Eliminar_pv", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("nId",nId);
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo ingresar el registro";


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if(sqlCon.State == ConnectionState.Open) sqlCon.Close(); 
            }
            return respuesta;
        }
    }
}
