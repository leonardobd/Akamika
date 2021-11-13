using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_Entidades;
using C_ReglasNegocio;
using System.IO;
namespace C_Presentacion.Controllers
{
    public class PersonaController : Controller
    {
        #region Sesion del usuario
        public Int16 SesionUsuario()
        {
            Usuario u = (Usuario)Session["usuario"];
            Int16 ID = Convert.ToInt16(u.UsuarioID);
            return ID;
        }
        #endregion

        #region Guardado de foto
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
        #endregion

        #region Mantenimiento de perfil de usuario
        public ActionResult PerfilUsuario(Int16 ?Identificador,String Mensaje)
        {
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            
            try
            {        
        
                Usuario u = rn_Usuario.Instancia.PerfilUsuario(SesionUsuario());
                ViewBag.sexo = u.Persona.PersonaSexo;
                return View(u);
            }
            catch (Exception e)
            {
                ViewBag.mensaje = e.Message;
                ViewBag.identificador = 1;
                return View();
            }
        }        
        [HttpPost]
        public ActionResult ActualizarPerfil(FormCollection frm, HttpPostedFileBase fotoi)
        {
            try
            {
                Int16 PersonaID = Convert.ToInt16(frm["txt_PersonaID"]);
                Int16 UsuarioID = Convert.ToInt16(frm["txt_UsuarioID"]);
                String Nombres = frm["txt_PersonaNombres"].ToString();
                String Apellidos = frm["txt_PersonaApellidos"].ToString();
                String Sexo = frm["cbo_Sexo"].ToString();
                int dia = Convert.ToInt16(frm["txt_FechaDia"]);
                int mes = Convert.ToInt16(frm["txt_FechaMes"]);
                int año = Convert.ToInt16(frm["txt_FechaAño"]);              
                String email = frm["txt_Correo"].ToString();
                String password = frm["txt_Password"].ToString();
                String foto = "";
                if (fotoi != null && fotoi.ContentLength > 0)
                {
                    foto = fotoi.FileName;
                }
                else
                {
                    foto = "";
                }
                int i = rn_Usuario.Instancia.ActualizacionUsuario(UsuarioID, PersonaID, Nombres, Apellidos, Sexo, dia,mes,año, email, password, foto);
                
                if (i == 1)
                {
                    int f = GuardarFoto(fotoi,"I_Usuario");
                    if (f == 1) {
                        return RedirectToAction("PerfilUsuario", "Persona", new { Identificador = 3, Mensaje = "Perfil actualizado correctamente." });
                    }
                    else
                    {
                        return RedirectToAction("PerfilUsuario", "Persona", new { Identificador = 2, Mensaje = "La fotografía no se ha copiado correctamente." });
                    }
                }
                else if (i == 2)
                {
                    return RedirectToAction("PerfilUsuario", "Persona", new { Identificador = 3, Mensaje = "Perfil actualizado correctamente." });
                }
                else
                {
                    return RedirectToAction("PerfilUsuario", "Persona", new {Identificador = 2,Mensaje = "Parece que tenemos problemas para actualizar el perfil."});
                }
                
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("PerfilUsuario", "Persona", new { Identificador = 2, Mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("PerfilUsuario", "Persona", new { Identificador = 1, Mensaje = e.Message });
            }
        }
        #endregion
    }
}
