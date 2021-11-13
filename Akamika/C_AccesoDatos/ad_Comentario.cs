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
    public class ad_Comentario
    {
        #region Singleton
        private static readonly ad_Comentario _Instancia = new ad_Comentario();
        public static ad_Comentario Instancia
        {
            get
            {
                return ad_Comentario._Instancia;
            }
        }
        #endregion Singleton

        #region Ingreso de comentario
        public int IngresoComentario(Int16 UsuarioID, Int16 IDCurso, String Comentario)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spRegistroComentario", cn);
                cmd.Parameters.AddWithValue("@IDUsuario", UsuarioID);
                cmd.Parameters.AddWithValue("@IDCurso", IDCurso);
                cmd.Parameters.AddWithValue("@Comentario", Comentario);
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

        #region Lista de comentarios por curso
        public List<Comentario> ListaComentarioPorCurso(Int32 IDCurso)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Comentario> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarComentarioCurso", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CursoID", IDCurso);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Comentario>();
                while (dr.Read())
                {
                    Comentario c = new Comentario();
                    c.ComentarioID = Convert.ToInt32(dr["ComentarioID"]);
                    c.ComentarioDetalle = dr["ComentarioDetalle"].ToString();
                    c.FechaRegistroCM = dr["FechaRegistroCM"].ToString();
                    Usuario u = new Usuario();
                    u.UsuarioID = Convert.ToInt32(dr["UsuarioID"]);
                    Persona p = new Persona();
                    p.PersonaNombres = dr["PersonaNombres"].ToString();
                    p.PersonaApellidos = dr["PersonaApellidos"].ToString();
                    p.PersonaFoto = dr["PersonaFoto"].ToString();
                    u.Persona = p;
                    c.Usuario = u;
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

        #region Lista de todos los comentarios
        public List<Comentario> ListaComentario()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Comentario> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarComentario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Comentario>();
                while (dr.Read())
                {
                    Comentario c = new Comentario();
                    c.ComentarioDetalle = dr["ComentarioDetalle"].ToString();
                    c.FechaRegistroCM = dr["FechaRegistroCM"].ToString();
                    Usuario u = new Usuario();
                    u.UsuarioID = Convert.ToInt32(dr["UsuarioID"]);
                    Persona p = new Persona();
                    p.PersonaNombres = dr["PersonaNombres"].ToString();
                    p.PersonaApellidos = dr["PersonaApellidos"].ToString();
                    p.PersonaFoto = dr["PersonaFoto"].ToString();
                    u.Persona = p;
                    c.Usuario = u;
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

    }
}
