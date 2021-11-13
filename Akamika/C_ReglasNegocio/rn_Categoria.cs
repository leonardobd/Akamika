using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using C_AccesoDatos;
namespace C_ReglasNegocio
{
    public class rn_Categoria
    {
        #region Singleton
        private static readonly rn_Categoria _Instancia = new rn_Categoria();
        public static rn_Categoria Instancia
        {
            get
            {
                return rn_Categoria._Instancia;
            }
        }
        #endregion Singleton
        /*C.R.U.D : Create Read Update Delete / Crear Leer Actualizar Eliminar
         */
        #region Ingreso de categoria: Función crear, función eliminar, funcion buscar, funcion actualizar
        public int IngresoCategoria(String CategoriaNombre, String CategoriaIcono)
        {
            try
            {

                if (CategoriaNombre.Equals("") || CategoriaNombre.Equals(" ") || CategoriaNombre.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar el nombre.");
                }
                if (CategoriaIcono.Equals("") || CategoriaIcono.Equals(" ") || CategoriaIcono.Equals(" "))
                {
                    CategoriaIcono = "noimageCat.png";
                }
                int i = ad_Categoria.Instancia.IngresoCategoria(CategoriaNombre, CategoriaIcono);
                if (i == -1)
                {
                    throw new ApplicationException("El nombre ya está registrado.");
                }
                else if (i == -2)
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
        public List<Categoria> ListaCategoria()
        {
            try
            {
                return ad_Categoria.Instancia.ListarCategoria();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EliminarCategoria(Int64 CategoriaID)
        {
            try
            {
                if (CategoriaID < 1)
                {
                    throw new ApplicationException("Tenemos problemas al capturar el indicador. Intentelo de nuevo.");
                }
                int i = ad_Categoria.Instancia.EliminarCategoria(CategoriaID);
                if (i == -2)
                {
                    throw new ApplicationException("Existen problemas de conexion, reporte el problema.");
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

        public List<Categoria> BuscarCategoria(Int16 CategoriaID)
        {
            try
            {
                if (CategoriaID < 1)
                {
                    throw new ApplicationException("No se ha podido capturar el identificador.");
                }
                List<Categoria> c = ad_Categoria.Instancia.BuscarCategoria(CategoriaID);
                return c;
            }
            catch (ApplicationException ae)
            {
                throw ae;
            }
            catch (Exception e) {
                throw e;
            }
        }

        public int ActualizarCategoria(Int16 CategoriaID, String CategoriaNombre, String CategoriaIcono)
        {
            try
            {
                if (CategoriaID < 1)
                {
                    throw new ApplicationException("Hay problemas para capturar el indice.");
                }
                if (CategoriaNombre.Equals("") || CategoriaNombre.Equals(" ") || CategoriaNombre.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar el nombre.");
                }
                int i = ad_Categoria.Instancia.ActualizacionCategoria(CategoriaID, CategoriaNombre, CategoriaIcono);                
                if (i == -2)
                {
                    throw new Exception("Existen problemas de conexión, intentelo más tarde.");
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
