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
    public class ad_CursoRegistro
    {
        #region Singleton
        private static readonly ad_CursoRegistro _Instancia = new ad_CursoRegistro();
        public static ad_CursoRegistro Instancia
        {
            get
            {
                return ad_CursoRegistro._Instancia;
            }
        }
        #endregion Singleton

        #region Detalle de la sesion
        public CursoRegistro DetalleSesion(Int16 SesionID,Int16 CursoID)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            CursoRegistro cr = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDetalleSesionUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SesionID", SesionID);
                cmd.Parameters.AddWithValue("@CursoID", CursoID);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cr = new CursoRegistro();
                    cr.CursoRegistroID = Convert.ToInt16(dr["CursoRegistroID"]);
                    cr.CursoRegistroEstado = dr["CursoRegistroEstado"].ToString();
                    
                    Sesion s = new Sesion();
                    s.SesionID = Convert.ToInt16(dr["SesionID"]);
                    s.SesionTitulo = dr["SesionTitulo"].ToString();
                    s.SesionDescripcion = dr["SesionDescripción"].ToString();
                    s.SesionVideo = dr["SesionVideo"].ToString();
                    s.FechaRegistroS = dr["FechaRegistroS"].ToString();
                    s.SesionEstado = dr["SesionEstado"].ToString();
                    cr.Sesion = s;
                    Curso c = new Curso();
                    c.CursoID = Convert.ToInt16(dr["CursoID"]);
                    c.CursoNombre = dr["CursoNombre"].ToString();
                    s.Curso = c;
                }
            }
            catch (Exception e) { throw e; }
            finally { cmd.Connection.Close(); }
            return cr;
        }
        #endregion

        #region Registro de sesion
        public int RegistroSesion(Int16 UsuarioID, Int16 IDSesion,Int16 CursoID)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spAgregarSesionRegistro", cn);
                cmd.Parameters.AddWithValue("@UsuarioID", UsuarioID);
                cmd.Parameters.AddWithValue("@SesionID", IDSesion);
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
