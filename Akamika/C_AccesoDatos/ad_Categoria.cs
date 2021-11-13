using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using C_Entidades;
namespace C_AccesoDatos
{
    public class ad_Categoria
    {
        #region Singleton
        private static readonly ad_Categoria _Instancia = new ad_Categoria();
        public static ad_Categoria Instancia
        {
            get
            {
                return ad_Categoria._Instancia;
            }
        }
        #endregion Singleton

        #region Listado de categorias
        public List<Categoria> ListarCategoria()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Categoria> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaCategoria", cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Categoria>();
                while (dr.Read())
                {
                    Categoria c = new Categoria();
                    c.CategoriaID = Convert.ToInt32(dr["CategoriaID"]);
                    c.CategoriaNombre = dr["CategoriaNombre"].ToString();
                    c.CategoriaEstado = dr["CategoriaEstado"].ToString();
                    c.CategoriaIcono = dr["CategoriaIcono"].ToString();
                    Lista.Add(c);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }
        #endregion

        #region Ingreso de categoria
        public int IngresoCategoria(String Nombre, String Imagen)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spRegistroCategoria", cn);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@Imagen", Imagen);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter m = new SqlParameter("@retorno", DbType.Int32);
                m.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(m);
                cn.Open();
                cmd.ExecuteNonQuery();
                int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion

        #region Eliminar categoria
        public int EliminarCategoria(Int64 CategoriaID)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminaCategoria", cn);
                cmd.Parameters.AddWithValue("CategoriaID", CategoriaID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter m = new SqlParameter("@retorno", DbType.Int32);
                m.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(m);
                cn.Open();
                cmd.ExecuteNonQuery();
                int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion

        #region Busqueda de categoria
        public List<Categoria> BuscarCategoria(Int16 CategoriaID)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Categoria> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDetalleCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDCategoria", CategoriaID);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Categoria>();
                while (dr.Read())
                {
                    Categoria c = new Categoria();
                    c.CategoriaID = Convert.ToInt32(dr["CategoriaID"]);
                    c.CategoriaNombre = dr["CategoriaNombre"].ToString();
                    c.CategoriaIcono = dr["CategoriaIcono"].ToString();
                    c.CategoriaEstado = dr["CategoriaEstado"].ToString();
                    c.FechaRegistroC = dr["FechaRegistroC"].ToString();
                    Lista.Add(c);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }
        #endregion

        #region Actualización de categoria
        public int ActualizacionCategoria(Int16 IDCategoria, String CategoriaNombre, String CategoriaIcono)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spActualizarCategoria", cn);
                cmd.Parameters.AddWithValue("@CategoriaID", IDCategoria);
                cmd.Parameters.AddWithValue("@CategoriaNombre", CategoriaNombre);
                cmd.Parameters.AddWithValue("@CategoriaIcono", CategoriaIcono);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter m = new SqlParameter("@retorno", DbType.Int32);
                m.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(m);
                cn.Open();
                cmd.ExecuteNonQuery();
                int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion
    }
}
