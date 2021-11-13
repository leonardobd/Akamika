using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Entidades;
using C_AccesoDatos;
namespace C_ReglasNegocio
{
    public class rn_Usuario
    {
        #region Singleton
        private static readonly rn_Usuario _Instancia = new rn_Usuario();
        public static rn_Usuario Instancia
        {
            get
            {
                return rn_Usuario._Instancia;
            }
        }
        #endregion Singleton

        #region Ingreso
        public Usuario Log_Sistema(String Correo, String Passw)
        {
            try
            {
                if (Correo == null || Correo == "" || Correo == " " || Correo == " ")
                {
                    throw new ApplicationException("Debe ingresar su direccion de correo.");
                }
                if (Passw == null || Passw == "" || Passw == " " || Passw == " ")
                {
                    throw new ApplicationException("Debe ingresar su contraseña.");
                }
                Usuario u = ad_Usuario.Instancia.Log_Sistema(Correo, Passw);
                if (u == null)
                {
                    throw new ApplicationException("Correo y/o contraseña incorrectos.");
                }
                if (u.UsuarioEstado == "I")
                {
                    throw new ApplicationException("Su cuenta aún no está activa. Verifique el correo con el que se registró.");
                }
                else if (u.UsuarioEstado == "D")
                {
                    throw new ApplicationException("Su cuenta ha sido desactivada por inflingir las normas. Comuniquese con el responsable de seguridad por medio del link que dice Reportar.");
                }
                return u;
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

        #region Registro
        public int Registro(String Nombres, String Apellidos, String Sexo, String FechaNacimiento, String email, String password, String foto)
        {
            try
            {
                if (Nombres.Equals("") || Nombres.Equals(" ") || Nombres.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su(s) nombre(s).");
                }
                if (Apellidos.Equals("") || Apellidos.Equals(" ") || Apellidos.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su(s) apellido(s).");
                }
                if (Sexo.Equals("") || Sexo.Equals(" ") || Sexo.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su genero.");
                }
                if (FechaNacimiento.Equals("") || FechaNacimiento.Equals(" ") || FechaNacimiento.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su fecha de nacimiento.");
                }
                if (email.Equals("") || email.Equals(" ") || email.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su correo.");
                }
                if (password.Equals("") || password.Equals(" ") || password.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su contraseña.");
                }
                int i = ad_Usuario.Instancia.Registro(Nombres, Apellidos, Sexo, FechaNacimiento, email, password, foto);
                if (i == -1)
                {
                    throw new ApplicationException("El correo que usted está usando ya está registrado.");
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
        #endregion

        #region Perfil de usuario
        public Usuario PerfilUsuario(Int16 IDUsuario)
        {
            try
            {
                Usuario u = ad_Usuario.Instancia.PerfilUsuario(IDUsuario);
                return u;
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

        #region Actualizacion de usuario
        public int ActualizacionUsuario(Int16 UsuarioID, Int16 PersonaID, String Nombres, String Apellidos, String Sexo, int dia, int mes, int año, String email, String password, String foto)
        {
            try
            {
                if (Nombres.Equals("") || Nombres.Equals(" ") || Nombres.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su(s) nombre(s).");
                }else if (Apellidos.Equals("") || Apellidos.Equals(" ") || Apellidos.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su(s) apellido(s).");
                }else if (Sexo.Equals("") || Sexo.Equals(" ") || Sexo.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su genero.");
                }
                else if (dia<1 || mes <1|| año<1060 || dia>31||mes>12||año>9999)
                {
                    throw new ApplicationException("Formato no aceptado.");                    
                }
                else if (email.Equals("") || email.Equals(" ") || email.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su correo.");
                }
                else if (password.Equals("") || password.Equals(" ") || password.Equals(" "))
                {
                    throw new ApplicationException("Debe ingresar su contraseña.");
                }
                else if (UsuarioID < 1)
                {
                    throw new Exception("Error al intentar obtener el identificador. Vuelva a iniciar sesión.");
                }
                else if (PersonaID < 1)
                {
                    throw new Exception("Error al intentar obtener el identificador. Vuelva a iniciar sesión");
                }
                String FechaNacimiento = dia + "/" + mes + "/" + año;
                int i = ad_Usuario.Instancia.ActualizacionUsuario(UsuarioID, PersonaID, Nombres, Apellidos, Sexo, FechaNacimiento, email, password, foto);
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

        /*Desarrollado por LBD de la página de www.codcafein.wordpress.com: Ésta página no colabora con ninguna otra, 
          y el material que se comparte es puramente educativo. Se prohibe vender este material, repartir como si fuese 
          de su propiedad o gananar crédito con ella.*/
    }
}
