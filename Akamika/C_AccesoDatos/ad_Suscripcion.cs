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
    public class ad_Suscripcion
    {
        #region Singleton
        private static readonly ad_Suscripcion _Instancia = new ad_Suscripcion();
        public static ad_Suscripcion Instancia
        {
            get
            {
                return ad_Suscripcion._Instancia;
            }
        }
        #endregion Singleton

        #region Suscripcion
        public int Suscripcion(String Correo)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spAgregarSuscripcion", cn);
                cmd.Parameters.AddWithValue("@SuscritoCorreo", Correo);
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
