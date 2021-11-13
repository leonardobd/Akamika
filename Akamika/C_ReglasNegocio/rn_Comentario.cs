using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using C_AccesoDatos;
namespace C_ReglasNegocio
{
    public class rn_Comentario
    {
        #region Singleton
        private static readonly rn_Comentario _Instancia = new rn_Comentario();
        public static rn_Comentario Instancia
        {
            get
            {
                return rn_Comentario._Instancia;
            }
        }
        #endregion Singleton

        #region Lista de comentarios por curso
        public List<Comentario> ListaComentarioPorCurso(Int32 IDCurso)
        {
            try
            {
                return ad_Comentario.Instancia.ListaComentarioPorCurso(IDCurso);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        
        /*C.R. : Create Read  / Crear Leer 
         */
        #region Ingreso de comentario: Función crear
        public int IngresoComentario(Int16 IDUsuario, Int16 IDCurso, String Comentario)
        {
            try
            {
                if (IDUsuario<0)
                {
                    throw new ApplicationException("Debe iniciar sesión.");
                }
                if (IDCurso<0)
                {
                    throw new Exception("Error al capturar el índice del curso.");
                }
                if (Comentario.Equals("") || Comentario.Equals(" ") || Comentario.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar un comentario.");
                }                
                int i = ad_Comentario.Instancia.IngresoComentario(IDUsuario, IDCurso, Comentario);
                if (i == -2)
                {
                    throw new Exception("Existen problemas de conexión, reporte el problema.");
                }
                return i;
            }
            catch (ApplicationException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
              
        #region Lista de comentarios: Función leer
        public List<Comentario> ListaComentario()
        {
            try
            {
                return ad_Comentario.Instancia.ListaComentario();
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
