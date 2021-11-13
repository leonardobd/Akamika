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
    public class ad_Usuario
    {
        #region Singleton
        private static readonly ad_Usuario _Instancia = new ad_Usuario();
        public static ad_Usuario Instancia
        {
            get
            {
                return ad_Usuario._Instancia;
            }
        }
        #endregion Singleton

        #region Ingreso
        public Usuario Log_Sistema(String correo, String password)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Usuario u = null;
            Persona p = null;
            TipoUsuario tu = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spValidacionUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Password", password);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new Usuario();
                    u.UsuarioID = Convert.ToInt32(dr["UsuarioID"]);
                    u.UsuarioCorreo = dr["UsuarioCorreo"].ToString();
                    u.UsuarioEstado = dr["UsuarioEstado"].ToString();
                    tu = new TipoUsuario();
                    tu.TipoUsuarioID = Convert.ToInt16(dr["UsuarioTipo"]);
                    u.UsuarioTipo = tu;
                    p = new Persona();
                    p.PersonaApellidos = dr["PersonaApellidos"].ToString();
                    p.PersonaNombres = dr["PersonaNombres"].ToString();
                    p.PersonaFoto = dr["PersonaFoto"].ToString();
                    p.PersonaEstado = dr["PersonaEstado"].ToString();
                    u.Persona = p;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { cmd.Connection.Close(); }
            return u;
        }
        #endregion

        #region Registro
        public int Registro(String Nombres, String Apellidos, String Sexo, String FechaNacimiento, String email, String password, String foto)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spRegistroUsuario", cn);
                cmd.Parameters.AddWithValue("@Nombres", Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", Apellidos);
                cmd.Parameters.AddWithValue("@Sexo", Sexo);
                cmd.Parameters.AddWithValue("@FechaNacimiento", FechaNacimiento);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Contraseña", password);
                cmd.Parameters.AddWithValue("@Foto", foto);
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

        #region Perfil de Usuario
        public Usuario PerfilUsuario(Int16 IDUsuario)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Usuario u = null;
            Persona p = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spPerfilUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDUsuario", IDUsuario);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new Usuario();
                    u.UsuarioID = Convert.ToInt16(dr["UsuarioID"]);
                    u.UsuarioCorreo = dr["UsuarioCorreo"].ToString();
                    u.UsuarioPassw = dr["UsuarioPassw"].ToString();
                    p = new Persona();
                    p.PersonaID = Convert.ToInt16(dr["PersonaID"]);
                    p.PersonaApellidos = dr["PersonaApellidos"].ToString();
                    p.PersonaNombres = dr["PersonaNombres"].ToString();
                    p.PersonaSexo = dr["PersonaSexo"].ToString();
                    p.PersonaFoto = dr["PersonaFoto"].ToString();
                    p.PersonaFechaNacimiento = Convert.ToDateTime(dr["PersonaFechaNacimiento"]);
                    u.Persona = p;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { cmd.Connection.Close(); }
            return u;
        }
        #endregion

        #region Actualizacion de Usuario
        public int ActualizacionUsuario(Int16 UsuarioID,Int16 PersonaID, String Nombres, String Apellidos, String Sexo, String FechaNacimiento, String email, String password, String foto)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spActualizarPerfil", cn);
                cmd.Parameters.AddWithValue("@PersonaID", UsuarioID);
                cmd.Parameters.AddWithValue("@UsuarioID", PersonaID);
                cmd.Parameters.AddWithValue("@UsuarioCorreo", email);
                cmd.Parameters.AddWithValue("@UsuarioPassword", password);
                cmd.Parameters.AddWithValue("@PersonaNombres", Nombres);
                cmd.Parameters.AddWithValue("@PersonaApellidos", Apellidos);
                cmd.Parameters.AddWithValue("@PersonaSexo", Sexo);
                cmd.Parameters.AddWithValue("@PersonaFoto", foto);
                cmd.Parameters.AddWithValue("@PersonaFechaNacimiento", Convert.ToDateTime(FechaNacimiento).ToShortDateString());
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
