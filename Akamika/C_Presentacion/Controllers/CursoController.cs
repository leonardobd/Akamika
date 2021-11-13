using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_Entidades;
using C_ReglasNegocio;
namespace C_Presentacion.Controllers
{
    public class CursoController : Controller
    {
        #region Sesion del usuario
        public Int16 SesionUsuario()
        {
            Usuario u = (Usuario)Session["usuario"];
            Int16 ID = Convert.ToInt16(u.UsuarioID);
            return ID;
        }
        #endregion

        #region Mantenimiento de curso
        public ActionResult CursoDetalle(Int16 CategoriaID)
        {
            return View(rn_Curso.Instancia.ListaCursosPorCategoria(CategoriaID));
        }
        
        public ActionResult CursoRegistrado()
        {
            return View(rn_Curso.Instancia.ListaCursoRegistrado(SesionUsuario()));
        }
        #endregion

        #region Mantenimiento de sesion
        public ActionResult SesionRegistrada(Int16 CursoID)
        {
            return View(rn_Sesion.Instancia.ListaSesionRegistrada(SesionUsuario(), CursoID));
        }
        public ActionResult DetalleSesion(Int16 SesionID, Int16 CursoID)
        {
            ViewBag.Curso = CursoID;
            try
            {
                CursoRegistro cr = rn_CursoRegistro.Instancia.DetalleSesion(SesionID, CursoID);
                return View(cr);
            }
            catch (ApplicationException ae)
            {
                ViewBag.mensaje = ae.Message;
                return RedirectToAction("FinSesiones", new { identificador = 2, mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("FinSesiones", new { identificador = 2, mensaje = e.Message });
            }
        }
        public ActionResult ListaSession(Int16 CursoID, Int16? Identificador, String Mensaje)
        {

            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            ViewBag.IDCurso = CursoID;
            return View(rn_Sesion.Instancia.ListaSession(CursoID));
        }
        public ActionResult FinSesiones(Int16? Identificador, String Mensaje)
        {
            ViewBag.mensaje = Mensaje;
            ViewBag.identificador = Identificador;
            return View();
        }
        public ActionResult Error(String Mensaje)
        {
            ViewBag.mensaje = Mensaje;
            return View();
        }
        public ActionResult RegistrarSesion(Int16 SesionID, Int16 CursoID)
        {
            try
            {
                rn_CursoRegistro.Instancia.RegistroSesion(SesionUsuario(), SesionID, CursoID);
                return RedirectToAction("DetalleSesion", new { SesionID = SesionID, CursoID = CursoID });

            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("FinSesiones", new { Identificador = 1, Mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { Mensaje = e.Message });
            }
        }
        #endregion

        #region Mantenimiento de comentario
        [HttpPost]
        public ActionResult IngresoComentario(FormCollection frm) {
            Int16 IDCurso = Convert.ToInt16(frm["txt_IDCurso"]);
            try
            {                
                String Comentario = frm["txt_Comentario"].ToString();
                int i = rn_Comentario.Instancia.IngresoComentario(SesionUsuario(),IDCurso , Comentario);
                return RedirectToAction("ListaSession", new { CursoID = IDCurso });
            }
            catch (ApplicationException ae)
            {
                ViewBag.mensaje = ae.Message;
                return RedirectToAction("ListaSession", new { CursoID = IDCurso, identificador = 2, mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaSession", new { CursoID = IDCurso, identificador = 1, mensaje = e.Message });
            }
        }
        #endregion

        
        
        
        
    }
}
