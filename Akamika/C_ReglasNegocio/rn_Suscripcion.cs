using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using C_AccesoDatos;
namespace C_ReglasNegocio
{
    public class rn_Suscripcion
    {
        #region Singleton
        private static readonly rn_Suscripcion _Instancia = new rn_Suscripcion();
        public static rn_Suscripcion Instancia
        {
            get
            {
                return rn_Suscripcion._Instancia;
            }
        }
        #endregion Singleton

        #region Suscripcion
        public int Suscripcion(String Correo)
        {
            try
            {
                if (Correo.Equals("") || Correo.Equals(" ") || Correo.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su correo.");
                }
                if (Correo.Length > 40)
                {
                    throw new ApplicationException("Su correo ah desbordado la cantidad de caracteres permitidos.");
                }
                int i = ad_Suscripcion.Instancia.Suscripcion(Correo);
                if (i == -1)
                {
                    throw new ApplicationException("El correo que usted está usando ya está registrado.");
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

        /*Desarrollado por LBD de la página de www.codcafein.wordpress.com: Ésta página no colabora con ninguna otra, 
          y el material que se comparte es puramente educativo. Se prohibe vender este material, repartir como si fuese 
          de su propiedad o gananar crédito con ella.*/
    }
}
