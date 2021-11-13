using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using C_AccesoDatos;

namespace C_ReglasNegocio
{
    public class rn_CursoRegistro
    {
        #region Singleton
        private static readonly rn_CursoRegistro _Instancia = new rn_CursoRegistro();
        public static rn_CursoRegistro Instancia
        {
            get
            {
                return rn_CursoRegistro._Instancia;
            }
        }
        #endregion Singleton

        #region Detalle de sesión
        public CursoRegistro DetalleSesion(Int16 SesionID,Int16 CursoID)
        {
            try
            {
                if (SesionID <= 0)
                {
                    throw new ApplicationException("No se ha captado el indice de la sesión.");
                }
                CursoRegistro cr = ad_CursoRegistro.Instancia.DetalleSesion(SesionID, CursoID);
                if (cr == null)
                {
                    throw new ApplicationException("No hay más sesiones por el momento.");
                }
                if (cr.Sesion.SesionEstado == "DE")
                {
                    throw new ApplicationException("La sesión está inactiva en estos momentos.");
                }
                else if (cr.Sesion.Curso.CursoEstado == "FN")
                {
                    throw new ApplicationException("Felicidades, ha terminado el curso satisfactoriamente.");
                }
                else if (cr.Sesion.SesionEstado == "EL")
                {
                    throw new ApplicationException("Esta sesión ha sido eliminada.");
                }
                return cr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        /*C. : Create / Crear
         */
        #region Registro de sesión: Función crear
        public int RegistroSesion(Int16 UsuarioID, Int16 SesionID,Int16 CursoID)
        {
            try
            {
                if (UsuarioID <= 0)
                {
                    throw new ApplicationException("Debe ingresar a su cuenta para poder registrarse.");
                }
                else if (SesionID <= 0)
                {
                    throw new ApplicationException("No se ha podido capturar la sesión.");
                }
                int rs = ad_CursoRegistro.Instancia.RegistroSesion(UsuarioID, SesionID,CursoID);
                if (rs == -2)
                {
                    throw new ApplicationException("Hay problemas al conectarse con la base de datos.");
                }
                else if (rs == -3)
                {
                    throw new ApplicationException("No hay nuevas sesiones registradas.");
                }
                return rs;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion 
        
        /*
         Desarrollado por LBD del blog de www.codcafein.wordpress.com: Esta web no colabora con ninguna otra, 
         y el material que se comparte es puramente educativo. Se prohibe vender el presente material, repartir 
         como si fuese de su propiedad o gananar crédito con ella. Sin embargo, pueden realizar modificaciones y 
         presentarlas como propias con la condición de nombrar al blog como desarrollador de la base y la creador
         del concepto.
         Pueden realizar consultas sobre el aplicativo en:
         www.facebook.com/codcafein
         visitar el blog:
         www.codcafein.wordpress.com
         o buscarnos en youtube como:
         Cod cafein
         */
    }
}
