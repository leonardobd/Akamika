using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using System.Data.SqlClient;
using System.Data;
namespace C_AccesoDatos
{
    public class ad_Sesion
    {
        #region Singleton
        private static readonly ad_Sesion _Instancia = new ad_Sesion();
        public static ad_Sesion Instancia
        {
            get
            {
                return ad_Sesion._Instancia;
            }
        }
        #endregion Singleton

        #region Listado de sesiones
        public List<Sesion> ListarSesion(Int32 CursoID)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Sesion> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaSesion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CursoID", CursoID);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Sesion>();
                while (dr.Read())
                {
                    Sesion s = new Sesion();
                    s.SesionID = Convert.ToInt32(dr["SesionID"]);
                    s.SesionTitulo = dr["SesionTitulo"].ToString();
                    s.FechaRegistroS = dr["FechaRegistroS"].ToString();
                    s.SesionEstado = dr["SesionEstado"].ToString();
                    Lista.Add(s);
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

        #region Lista de sesiones vistas
        public List<Sesion> ListaSesionRegistrado(Int32 IDUsuario,Int32 IDCurso)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Sesion> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sPListarSesionesRegistradas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", IDUsuario);
                cmd.Parameters.AddWithValue("@CursoID", IDCurso);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Sesion>();
                while (dr.Read())
                {
                    Sesion s = new Sesion();
                    s.SesionTitulo = dr["SesionTitulo"].ToString();
                    Lista.Add(s);
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

        #region Contador de videos
        public Sesion CantidadVideos()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Sesion s = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spCantidadVideos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    s = new Sesion();
                    s.SesionID = Convert.ToInt16(dr["SesionID"]);                    
                }
            }
            catch (Exception e) { throw e; }
            finally { cmd.Connection.Close(); }
            return s;
        }
        #endregion

        #region Sesion más vista
        public Sesion SesionMasVista()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Sesion s = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spSesionMasVista", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    s = new Sesion();
                    s.SesionID = Convert.ToInt16(dr["SesionID"]);
                    s.SesionVideo = dr["SesionVideo"].ToString();
                    s.SesionTitulo = dr["SesionTitulo"].ToString();
                }
                else
                {
                    s = new Sesion();
                    s.SesionID = 1;
                    s.SesionVideo = "";
                    s.SesionTitulo = "Aún sin definir";
                }
            }
            catch (Exception e) { throw e; }
            finally { cmd.Connection.Close(); }
            return s;
        }
        #endregion

        #region Eliminacion de sesion
        public int EliminarSesion(Int32 SesionID)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarSesion", cn);
                cmd.Parameters.AddWithValue("@SesionID", SesionID);
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

        #region Registro de curso
        public int RegistroSesion(String Titulo, String Descripcion, String Video,Int32 SesionID)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spAgregarSesion", cn);
                cmd.Parameters.AddWithValue("@Titulo", Titulo);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Video", Video);
                cmd.Parameters.AddWithValue("@IDCurso", SesionID);
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

        #region Actualizacion de sesiones
        public int ActualizarSesion(Int32 SesionID, String Descripción, String Titulo, String URL, String Publicacion)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spActualizarSesion", cn);
                cmd.Parameters.AddWithValue("@SesionID", SesionID);
                cmd.Parameters.AddWithValue("@Descripción", Descripción);
                cmd.Parameters.AddWithValue("@Titulo", Titulo);
                cmd.Parameters.AddWithValue("@Video", URL);
                cmd.Parameters.AddWithValue("@FechaPublicacion", Publicacion);
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

        #region Detalle sesion Intranet
        public List<Sesion> DetalleSesionIntranet(Int32 SesionID)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Sesion> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDetalleSesionIntranet", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SesionID", SesionID);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Sesion>();
                while (dr.Read())
                {
                    Sesion s = new Sesion();
                    s.SesionID = Convert.ToInt16(dr["SesionID"]);
                    s.SesionTitulo = dr["SesionTitulo"].ToString();
                    s.SesionDescripcion = dr["SesionDescripción"].ToString();
                    s.SesionVideo = dr["SesionVideo"].ToString();
                    s.SesionEstado = dr["SesionEstado"].ToString();
                    Lista.Add(s);
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

    }
}
