using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using C_AccesoDatos;
namespace C_ReglasNegocio
{
    public class rn_Sesion
    {
        #region Singleton
        private static readonly rn_Sesion _Instancia = new rn_Sesion();
        public static rn_Sesion Instancia
        {
            get
            {
                return rn_Sesion._Instancia;
            }
        }
        #endregion Singleton

        #region Lista de sesiones por curso
        public List<Sesion> ListaSession(Int32 CursoID)
        {
            try
            {
                return ad_Sesion.Instancia.ListarSesion(CursoID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        

        #region Cantidad de videos subidos
        public Sesion CantidadVideos()
        {
            try
            {
                return ad_Sesion.Instancia.CantidadVideos();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Video más visto
        public Sesion SesionMasVista()
        {
            try
            {
                Sesion s = ad_Sesion.Instancia.SesionMasVista();                
                return s;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        /*C.R.U.D. : Create Read Update Delete / Crear Leer Actualizar Eliminar
         */

        #region Registro de sesion: Función crear
        public int RegistroSesion(String Titulo, String Descripcion, String Video, Int32 IDCurso)
        {
            try
            {
                if (Titulo.Equals(null) || Titulo.Equals("") || Titulo.Equals(" ") || Titulo.Equals(" "))
                {
                    new ApplicationException("Debe llenar el título de la sesión.");
                }
                else if (Descripcion.Equals(null) || Descripcion.Equals("") || Descripcion.Equals(" ") || Descripcion.Equals(" "))
                {
                    Descripcion = "Sin descripción.";
                }
                else if (Video.Equals(null) || Video.Equals("") || Video.Equals(" ") || Video.Equals(" "))
                {
                    new ApplicationException("Debe llenar la URL del video (Debe ser URL de www.youtube.com).");
                }
                else if (IDCurso == 0)
                {
                    new ApplicationException("Parece que tenemos problemas al capturar el identificador del curso.");
                }
                int i = ad_Sesion.Instancia.RegistroSesion(Titulo, Descripcion, Video, IDCurso);
                if (i == -1)
                {
                    new ApplicationException("El título de la sesión ya esta registrado.");
                }
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Lista de sesiones registradas: Función leer
        public List<Sesion> ListaSesionRegistrada(Int32 IDUsuario, Int32 IDCurso)
        {
            try
            {
                return ad_Sesion.Instancia.ListaSesionRegistrado(IDUsuario, IDCurso);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Eliminar sesion: Funcion eliminar
        public int EliminarSesion(Int32 IDSesion)
        {
            try
            {
                int i = ad_Sesion.Instancia.EliminarSesion(IDSesion);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion        

        #region Actualizacion de sesion: Funcion actualizar
        public int ActualizarSesion(Int32 SesionID,String Titulo,String Descripcion,String URL,String Publicacion)
        {            
            try
            {
                if (Titulo.Equals(null) || Titulo.Equals("") || Titulo.Equals(" ") || Titulo.Equals(" "))
                {
                    new ApplicationException("Debe llenar el título de la sesión.");
                }
                else if (Descripcion.Equals(null) || Descripcion.Equals("") || Descripcion.Equals(" ") || Descripcion.Equals(" "))
                {
                    Descripcion = "Sin descripción.";
                }
                else if (URL.Equals(null) || URL.Equals("") || URL.Equals(" ") || URL.Equals(" "))
                {
                    new ApplicationException("Debe llenar la URL de la sesión.");
                }
                else if (SesionID < 1)
                {
                    new ApplicationException("No se ha podido capturar el identificador de la sesion. Intentelo denuevo.");
                }
                int i = ad_Sesion.Instancia.ActualizarSesion(SesionID, Descripcion, Titulo, URL, Publicacion);
                if (i == -2)
                {
                    new ApplicationException("Existen problemas de conexión, reporte el problema.");
                }
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Detalle de sesion: Funcion leer (Forma parte de la función actualizar)
        public List<Sesion> DetalleSesionIntranet(Int32 IDSesion)
        {
            try
            {
                return ad_Sesion.Instancia.DetalleSesionIntranet(IDSesion);
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
