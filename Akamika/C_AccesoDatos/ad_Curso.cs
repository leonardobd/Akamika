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
    public class ad_Curso
    {
        #region Singleton
        private static readonly ad_Curso _Instancia = new ad_Curso();
        public static ad_Curso Instancia
        {
            get
            {
                return ad_Curso._Instancia;
            }
        }
        #endregion Singleton

        #region Lista de curso
        public List<Curso> ListaCursosPorCategoria(Int16 CategoriaID)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Curso> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaCursosPorCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoriaID", CategoriaID);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Curso>();
                while (dr.Read())
                {
                    Curso c = new Curso();
                    c.CursoID = Convert.ToInt32(dr["CursoID"]);
                    c.CursoNombre = dr["CursoNombre"].ToString();
                    c.CursoImagen = dr["CursoImagen"].ToString();
                    c.CursoDescripción = dr["CursoDescripción"].ToString();
                    Categoria ct = new Categoria();
                    ct.CategoriaID = Convert.ToInt32(dr["CategoriaID"]);
                    ct.CategoriaImagen = dr["CategoriaImagen"].ToString();
                    c.Categoria = ct;
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

        #region Lista de cursos registrados por usuario
        public List<Curso> ListaCursoRegistrado(Int16 IDUsuario)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Curso> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarCursosRegistrados", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", IDUsuario);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Curso>();
                while (dr.Read())
                {
                    Curso c = new Curso();
                    c.CursoID = Convert.ToInt32(dr["CursoID"]);
                    c.CursoNombre = dr["CursoNombre"].ToString();
                    c.CursoImagen = dr["CursoImagen"].ToString();
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

        #region Registro de curso
        public int RegistroCurso(String CursoTitulo, String CursoDescripcion, String CursoImagen, Int16 CategoriaID, String FechaPublicacion)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spRegistroCurso", cn);
                cmd.Parameters.AddWithValue("@TituloCurso", CursoTitulo);
                cmd.Parameters.AddWithValue("@Descripcion", CursoDescripcion);
                cmd.Parameters.AddWithValue("@Imagen", CursoImagen);
                cmd.Parameters.AddWithValue("@CategoriaID", CategoriaID);
                cmd.Parameters.AddWithValue("@FechaPublic", FechaPublicacion);
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

        #region Lista de cursos
        public List<Curso> ListaCursos()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Curso> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaCursos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Curso>();
                while (dr.Read())
                {
                    Curso c = new Curso();
                    c.CursoID = Convert.ToInt32(dr["CursoID"]);
                    c.CursoNombre = dr["CursoNombre"].ToString();
                    c.CursoImagen = dr["CursoImagen"].ToString();
                    c.CursoDescripción = dr["CursoDescripción"].ToString();
                    c.CursoEstado = dr["CursoEstado"].ToString();
                    Categoria ct = new Categoria();
                    ct.CategoriaID = Convert.ToInt32(dr["CategoriaID"]);
                    ct.CategoriaImagen = dr["CategoriaImagen"].ToString();
                    c.Categoria = ct;
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

        #region Detalle de curso
        public List<Curso> DetalleCurso(Int32 CursoID)
        { 
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Curso> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDetalleCurso", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDCurso", CursoID);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<Curso>();
                while (dr.Read())
                {
                    Curso c = new Curso();
                    c.CursoID = Convert.ToInt32(dr["CursoID"]);
                    c.CursoNombre = dr["CursoNombre"].ToString();
                    c.CursoImagen = dr["CursoImagen"].ToString();
                    c.CursoDescripción = dr["CursoDescripción"].ToString();
                    Categoria ct = new Categoria();
                    ct.CategoriaID = Convert.ToInt32(dr["CategoriaID"]);
                    c.Categoria = ct;
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

        #region Actualizacion de curso
        public int ActualizacionCurso(Int16 IDCurso, String CursoTitulo, String CursoDescripcion, String CursoImagen, Int16 CategoriaID,String Publicacion)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spActualizarCurso", cn);
                cmd.Parameters.AddWithValue("@CursoID", IDCurso);
                cmd.Parameters.AddWithValue("@CursoDescripción", CursoDescripcion);
                cmd.Parameters.AddWithValue("@CursoImagen", CursoImagen);
                cmd.Parameters.AddWithValue("@CursoNombre", CursoTitulo);
                cmd.Parameters.AddWithValue("@CategoriaID", CategoriaID);
                cmd.Parameters.AddWithValue("@FechaPublic", Publicacion);
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

        #region Eliminación de curso
        public int EliminarCurso(Int64 CursoID)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarCurso", cn);
                cmd.Parameters.AddWithValue("@CursoID", CursoID);
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

