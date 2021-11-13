using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_Entidades;
using C_ReglasNegocio;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
namespace C_Presentacion.Controllers
{
    public class InicioController : Controller
    {
        #region Inicio
        public ActionResult Index(Int16? Identificador, String Mensaje)
        {
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            return View();
        }
        #endregion

        #region Vista de registro | login
        public ActionResult RegistroLogin(Int16? Identificador, String Mensaje)
        {
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            return View();
        }
        #endregion 

        #region Login
        public ActionResult IngresoUsuario(FormCollection frm)
        {
            try
            {
                String correo = frm["txtCorreo"].ToString();
                String passw = frm["txtPassw"].ToString();
                Usuario u = rn_Usuario.Instancia.Log_Sistema(correo, passw);
                Session["usuario"] = u;
                if (u.UsuarioTipo.TipoUsuarioID == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Principal", "IntranetInicio");
                }
                
            }
            catch (ApplicationException ae)
            {
                ViewBag.mensaje = ae.Message;
                return RedirectToAction("RegistroLogin", new { identificador = 2, mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("RegistroLogin", new { identificador = 1, mensaje = e.Message });
            }
        }
        #endregion
        
        #region Registrarse
        [HttpPost]
        public ActionResult RegistroUsuario(FormCollection frm, HttpPostedFileBase fotoi)
        {
            try
            {
                String nombres = frm["txtNombres"].ToString();
                String apellidos = frm["txtApellidos"].ToString();
                String sexo = frm["cboGenero"].ToString();
                String fechaNacimiento = frm["txtFechaNacimiento"].ToString();
                String email = frm["txtEmail"].ToString();
                String passw = frm["txtPassw"].ToString();
                String foto = "";
                if (fotoi != null && fotoi.ContentLength > 0)
                {
                    foto = fotoi.FileName;
                }
                else
                {
                    foto = "";
                }
                int i = rn_Usuario.Instancia.Registro(nombres, apellidos, sexo, fechaNacimiento, email, passw, foto);
                if (i == 1)
                {
                    int f = GuardarFoto(fotoi, "I_Usuario");
                    if (f == 1)
                    {
                        String mensaje = "Bienvenido " + nombres + ", se ha registrado correctamente a akamika.com.";
                        Correo("Cuenta de Akamika.com", email, mensaje);
                        return RedirectToAction("RegistroLogin", new { Identificador = 3, Mensaje = "Usted ah sido registrado correctamente." });
                    }
                    else
                    {
                        String mensaje = "Bienvenido " + nombres + ", se ha registrado correctamente a akamika.com.";
                        Correo("Cuenta de Akamika.com", email, mensaje);
                        return RedirectToAction("RegistroLogin", new { Identificador = 2, Mensaje = "Hemos tenido problemas al guardar la imagen. Intentelo más tarde." });
                    }
                }
                else if (i == 2)
                {
                    return RedirectToAction("RegistroLogin", new { Identificador = 3, Mensaje = "Usted ah sido registrado correctamente." });
                }
                else
                {
                    return RedirectToAction("RegistroLogin", new { Identificador = 2, Mensaje = "No se ha podido registrar correctamente, reporte este error con el administrador." });
                }                
            }
            catch (ApplicationException ae)
            {
                ViewBag.mensaje = ae.Message;
                return RedirectToAction("RegistroLogin", new { identificador = 2, mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("RegistroLogin", new { identificador = 1, mensaje = e.Message });
            }
        }
        public int GuardarFoto(HttpPostedFileBase fotoi, String Carpeta)
        {
            int e = 0;
            if (fotoi != null && fotoi.ContentLength > 0)
            {
                var namearchivo = Path.GetFileName(fotoi.FileName);

                var ruta = Path.Combine(Server.MapPath("~/desing/Imagenes/" + Carpeta), namearchivo);
                fotoi.SaveAs(ruta);
                e = 1;
            }
            return e;
        }
        public void Correo(String Asunto,String email,String Mensaje)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("porta_zpu_05@hotmail.com", "Akamika", Encoding.UTF8);
                mail.Subject = Asunto.ToString() ;
                mail.Body = Mensaje.ToString();
                mail.To.Add(email.ToString());
                smtpServer.Port = 25;
                smtpServer.Host = "smtp.live.com";
                smtpServer.Credentials = new System.Net.NetworkCredential("porta_zpu_05@hotmail.com", "portazpu1.");
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Cerrar sesión
        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Index");
        }
        #endregion

        #region Suscribirse
        public ActionResult Suscribir(FormCollection frm)
        {
            String correo = frm["txtCorreo"].ToString();
            try
            {
                int i = rn_Suscripcion.Instancia.Suscripcion(correo);
                Correo("Suscrito a Akamika",correo, "Su sucripción a akamika.com se ha realizado correctamente.");
                return RedirectToAction("Index", new { identificador = 3, mensaje = "Su correo ah sido registrado correctamente." });                
            }
            catch (ApplicationException ae)
            {
                ViewBag.mensaje = ae.Message;
                return RedirectToAction("Index", new { identificador = 2, mensaje = ae.Message });
            }
            catch (Exception e)
            {
                Correo("Error al registrar su correo.",correo, "Parece que hemos renido problemas al registrarlo. Intentelo de nuevo más tarde.");
                return RedirectToAction("Error", new { identificador = 1, mensaje = e.Message });
            }
        }
        #endregion 

        
    }
}
