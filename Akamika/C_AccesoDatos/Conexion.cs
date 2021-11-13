using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace C_AccesoDatos
{
    public class Conexion
    {
        #region Singleton
        private static readonly Conexion _Instancia = new Conexion();
        public static Conexion Instancia
        {
            get
            {
                return Conexion._Instancia;
            }
        }
        #endregion Singleton

        #region Cadena de conexion
        public SqlConnection Conectar()
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-FONOBII\\SQLEXPRESS;Initial Catalog=Akamika;Persist Security Info=True;User ID=sa;Password=123456;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
            return cn;
        }
        #endregion
    }
}
