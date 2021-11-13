using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using C_AccesoDatos;
namespace C_ReglasNegocio
{
    public class rn_Curso
    {
        #region Singleton
        private static readonly rn_Curso _Instancia = new rn_Curso();
        public static rn_Curso Instancia
        {
            get
            {
                return rn_Curso._Instancia;
            }
        }
        #endregion Singleton

        #region Listado de cursos por categoría
        public List<Curso> ListaCursosPorCategoria(Int16 CategoriaID)
        {
            try
            {
                return ad_Curso.Instancia.ListaCursosPorCategoria(CategoriaID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

       
        #region Lista de cursos registrados por usuario
        public List<Curso> ListaCursoRegistrado(Int16 IDUsuario)
        {
            try
            {
                return ad_Curso.Instancia.ListaCursoRegistrado(IDUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion 
        
        /*C.R.U.D. : Create Read Update Delete / Crear Leer Actualizar Eliminar
         */
        #region Registro de curso: Función crear
        public int RegistroCurso(String CursoTitulo, String CursoDescripcion, String CursoImagen, Int16 CategoriaID,String FechaPublicacion)
        {
            try
            {
                if (CursoTitulo.Equals(null) || CursoTitulo.Equals("") || CursoTitulo.Equals(" ") || CursoTitulo.Equals(" "))
                {
                    new ApplicationException("Debe llenar el título del curso.");
                }
                else if (CursoDescripcion.Equals(null) || CursoDescripcion.Equals("") || CursoDescripcion.Equals(" ") || CursoDescripcion.Equals(" "))
                {
                    CursoDescripcion = "Sin descripción.";
                }
                else if (CategoriaID<1)
                {
                    new ApplicationException("No se ha podido capturar el identificador de la categoría. Intentelo denuevo.");
                }
                int i = ad_Curso.Instancia.RegistroCurso(CursoTitulo, CursoDescripcion, CursoImagen, CategoriaID, FechaPublicacion);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Lista de cursos
        public List<Curso> ListaCursos()
        {
            try
            {
                return ad_Curso.Instancia.ListaCursos();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Detalle de curso: Función leer (Forma parte de la función actualizar)
        public List<Curso> DetalleCurso(Int32 IDCurso)
        {
            try {
                return ad_Curso.Instancia.DetalleCurso(IDCurso); 
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        #endregion

        #region Actualización de curso: Función actualizar
        public int ActualizacionCurso(Int16 IDCurso, String CursoTitulo, String CursoDescripcion, String CursoImagen, Int16 CategoriaID,String Publicacion)
        {
            try
            {
                if (CategoriaID < 0)
                {
                    new ApplicationException("Debe escoger una categoria");
                }
                int i = ad_Curso.Instancia.ActualizacionCurso(IDCurso, CursoTitulo, CursoDescripcion, CursoImagen, CategoriaID, Publicacion);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Eliminar curso: Funcion eliminar
        public int EliminarCurso(Int64 IDCurso)
        {
            try
            {
                int i = ad_Curso.Instancia.EliminarCurso(IDCurso);
                return i;
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
