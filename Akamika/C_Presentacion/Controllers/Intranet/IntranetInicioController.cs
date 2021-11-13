using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_Entidades;
using C_ReglasNegocio;
using System.IO;
namespace C_Presentacion.Controllers.Intranet
{
    public class IntranetInicioController : Controller
    {
        #region Guardar fotos
        public int GuardarFoto(HttpPostedFileBase fotoi,String Carpeta)
        {
            int e = 0;
            if (fotoi != null && fotoi.ContentLength > 0)
            {
                var namearchivo = Path.GetFileName(fotoi.FileName);

                var ruta = Path.Combine(Server.MapPath("~/desing/Imagenes/"+Carpeta), namearchivo);
                fotoi.SaveAs(ruta);
                e = 1;
            }
            return e;
        }
        #endregion

        #region Sesion del usuario
        public Int16 SesionUsuario()
        {
            Usuario u = (Usuario)Session["usuario"];
            Int16 ID = Convert.ToInt16(u.UsuarioID);
            return ID;
        }
        #endregion 

        #region Vista Principal
        public ActionResult Principal()
        {
            Sesion s = rn_Sesion.Instancia.CantidadVideos();
            ViewBag.CVideos = s.SesionID;
            Sesion sv = rn_Sesion.Instancia.SesionMasVista();
            return View(sv);
        }
        #endregion

        #region Mantenimiento de cursos
        public ActionResult MantenimientoCursos(Int32? Identificador, String Mensaje)
        {
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            return View(rn_Curso.Instancia.ListaCursos());
        }


        public ActionResult ActualizarCurso(Int32 CursoID, Int16? Identificador, String Mensaje)
        {
            ViewBag.IDCurso = CursoID;
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            List<Curso> c = rn_Curso.Instancia.DetalleCurso(CursoID);
            return View(c);
        }
        [HttpPost]
        public ActionResult ActualizacionCurso(FormCollection frm, HttpPostedFileBase img)
        {
            Int32 Cursoid = Convert.ToInt32(frm["txtCursoID"]);
            try
            {
                
                String titulo = frm["Nombre"].ToString();
                String descripcion = frm["Descripcion"].ToString();
                Int16 categoria = Convert.ToInt16(frm["idCategoria"]);
                Int16 curso = Convert.ToInt16(frm["idCurso"]);
                String imagen ="";
                String publicacion = frm["txt_PFecha"] + " " + frm["txt_PHora"];
                if (img != null && img.ContentLength > 0)
                {
                    imagen = img.FileName;
                }
                else
                {
                    imagen = "";
                }
                int i = rn_Curso.Instancia.ActualizacionCurso(curso,titulo, descripcion, imagen, categoria,publicacion);
                if (i == 1)
                {
                    int f = GuardarFoto(img, "I_Curso");
                    if(f==1){
                        return RedirectToAction("MantenimientoCursos", new { identificador = 3, mensaje = "Curso actualizado correctamente." });
                    }
                    else
                    {
                        return RedirectToAction("ActualizarCurso", new { CursoID = Cursoid, identificador = 2, mensaje = "No se ha podido actualizar la imagen correctamente." });
                    }
                    }
                else if (i == 2|| i ==3)
                {
                    return RedirectToAction("MantenimientoCursos", new { identificador = 3, mensaje = "Curso actualizado correctamente." });
                }

                else
                {
                    return RedirectToAction("ActualizarCurso", new { CursoID = Cursoid, identificador = 2, mensaje = "Parece que hay errores al intentar actualizar el curso." });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ActualizarCurso", new { CursoID = Cursoid, identificador = 1, mensaje = e.Message });
            }
        }
        public ActionResult AgregarCurso(FormCollection frm, HttpPostedFileBase img)
        {
            try
            {
                String CursoTitulo = frm["Nombre"].ToString();
                String CursoDescripcion = frm["Descripcion"].ToString();
                Int16 CategoriaID = Convert.ToInt16(frm["idCategoria"]);
                String FechaPublicacion = frm["txt_PFecha"] + " "+frm["txt_PHora"];
                String CursoImagen = "";
                if (img != null && img.ContentLength > 0)
                {
                    CursoImagen = img.FileName;
                }
                else
                {
                    CursoImagen = "";
                }
                int i = rn_Curso.Instancia.RegistroCurso(CursoTitulo, CursoDescripcion, CursoImagen, CategoriaID,FechaPublicacion);
                
                if (i == 1)
                {
                    int f = GuardarFoto(img, "I_Curso");
                    if (f == 1)
                    {
                        return RedirectToAction("MantenimientoCursos", new { identificador = 3, mensaje = "Curso agregado correctamente." });
                    }
                    else
                    {
                        return RedirectToAction("MantenimientoCursos", new { identificador = 2, mensaje = "No se ha podido agregar la imagen correctamente." });
                    }                    
                }
                else if (i == 2 || i == 3)
                {
                    return RedirectToAction("MantenimientoCursos", new { identificador = 3, mensaje = "Curso agregado correctamente." });
                }
                else
                {
                    return RedirectToAction("MantenimientoCursos", new { identificador = 2, mensaje = "Parece que hay errores al intentar registrar el curso." });
                }
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("MantenimientoCursos", new { identificador = 2, mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("MantenimientoCursos", new { identificador = 1, mensaje = e.Message });
            }

        }

        public ActionResult EliminarCurso(FormCollection frm)
        {
            try
            {
                Int32 CursoID = Convert.ToInt32(frm["identificador"]);
                int i = rn_Curso.Instancia.EliminarCurso(CursoID);
                return RedirectToAction("MantenimientoCursos", "IntranetInicio", new { Identificador = 3, Mensaje = "Se ha eliminado el curso." });
            }
            catch (Exception e)
            {
                return RedirectToAction("MantenimientoCursos", "IntranetInicio", new { Identificador = 1, Mensaje = e.Message });
            }
        }
        #endregion              

        #region Mantenimiento de perfil
        public ActionResult Perfil(Int16 ?Identificador,String Mensaje)
        {
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            try
            {                
                Usuario u = rn_Usuario.Instancia.PerfilUsuario(SesionUsuario());
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
                    int f = GuardarFoto(fotoi, "I_Usuario");
                    if (f == 1)
                    {
                        return RedirectToAction("Perfil", "IntranetInicio", new { Identificador = 3, Mensaje = "Perfil actualizado correctamente." });
                    }
                    else
                    {
                        return RedirectToAction("Perfil", "IntranetInicio", new { Identificador = 2, Mensaje = "La fotografía no se ha copiado correctamente." });
                    }
                }
                else if (i == 2)
                {
                    return RedirectToAction("Perfil", "IntranetInicio", new { Identificador = 3, Mensaje = "Perfil actualizado correctamente." });
                }               
                else
                {
                    return RedirectToAction("Perfil", "IntranetInicio", new { Identificador = 2, Mensaje = "Parece que tenemos problemas para actualizar el perfil." });
                } 
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("Perfil", "IntranetInicio", new { Identificador = 2, Mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("Perfil", "IntranetInicio", new { Identificador = 1, Mensaje = e.Message });
            }
        }
        #endregion

        #region Mantenimiento de sesion
        public ActionResult ModificarSesion(Int32 CursoID, Int16 SesionID,Int16 ?Identificador, String Mensaje)
        {
            ViewBag.idCurso = CursoID;
            ViewBag.sesionID = SesionID;
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            List<Sesion> s = rn_Sesion.Instancia.DetalleSesionIntranet(SesionID);
            return View(s);
        }

        public ActionResult ModificacionSesion(FormCollection frm)
        {
            Int32 CursoID = Convert.ToInt16(frm["curso"]);
            Int32 SesionID = Convert.ToInt16(frm["sesion"]);
            try
            {
                String Titutlo = frm["titulo"].ToString();
                String Descripcion = frm["descripcion"].ToString();
                String URL = frm["URL"].ToString();
                String Publicacion = frm["txt_PFecha"] + " " + frm["txt_PHora"];
                int i = rn_Sesion.Instancia.ActualizarSesion(SesionID, Titutlo, Descripcion, URL,Publicacion);
                return RedirectToAction("ModificarSesion", "IntranetInicio", new { CursoID = CursoID, SesionID = SesionID, Identificador = 3, Mensaje = "Sesión actualizada correctamente." });
            }
            catch (Exception e)
            {
                return RedirectToAction("ModificarSesion", "IntranetInicio", new { CursoID = CursoID, SesionID=SesionID, Identificador = 1, Mensaje = e.Message });
            }
        }

        public ActionResult AgregarSesion(FormCollection frm)
        {
            Int16 CursoID = Convert.ToInt16(frm["idCurso"]);
            try
            {
                String Titulo = frm["titulo"].ToString();
                String Detalle = frm["Descripcion"].ToString();
                String URL = frm["URL"].ToString();
                int i = rn_Sesion.Instancia.RegistroSesion(Titulo, Detalle, URL, CursoID);
                return RedirectToAction("ActualizarSesiones", "IntranetInicio", new { CursoID = CursoID, Identificador = 3, Mensaje = "Sesión agregada correctamente." });
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("ActualizarSesiones", "IntranetInicio", new { CursoID = CursoID, Identificador = 2, Mensaje = ae.Message });
            }
            catch (Exception e) {
                return RedirectToAction("ActualizarSesiones", "IntranetInicio", new { CursoID = CursoID, Identificador = 1, Mensaje = e.Message });
            }
        }

        public ActionResult EliminarSesion(Int32 CursoID,Int32 SesionID)
        {
            try
            {
                int i = rn_Sesion.Instancia.EliminarSesion(SesionID);
                return RedirectToAction("ActualizarSesiones", "IntranetInicio", new { CursoID= CursoID,Identificador = 3, Mensaje = "Se ha eliminado la sesión correctamente." });
            }
            catch (Exception e)
            {
                return RedirectToAction("ActualizarSesiones", "IntranetInicio", new {CursoID= CursoID, Identificador = 1, Mensaje = e.Message });
            }
        }

        public ActionResult SubMantenimientos(Int16 ?Identificador , String Mensaje)
        {
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            return View();
        }

        public ActionResult ActualizarSesiones(Int16 CursoID, Int16? Identificador, String Mensaje)
        {
            ViewBag.idCurso = CursoID;
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            return View(rn_Sesion.Instancia.ListaSession(CursoID));
        }
        #endregion

        #region Mantenimiento de categoría
        public ActionResult RegistarCategoria(FormCollection frm, HttpPostedFileBase fotoi)
        {
            try
            {
                String nombre = frm["nombre"].ToString();
                String foto = "";
                if (fotoi != null && fotoi.ContentLength > 0)
                {
                    foto = fotoi.FileName;
                }
                else
                {
                    foto = "";
                }
                int i = rn_Categoria.Instancia.IngresoCategoria(nombre, foto);
                if (i ==1)
                {
                    int f = GuardarFoto(fotoi, "I_Categoria");
                    if (f == 1)
                    {
                        return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 3, Mensaje = "Se ha agregado la categoría correctamente." });
                    }
                    else
                    {
                        return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 2, Mensaje = "La imagen no se ha copiado correctamente." });
                    }
                    
                }
                else if (i == 2)
                {
                    return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 3, Mensaje = "Se ha agregado la categoría correctamente." });
                }
                else
                {
                    return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 2, Mensaje = "Parece que tenemos problemas para actualizar el perfil." });
                }               
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 2, Mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 1, Mensaje = e.Message });
            }
        }

        public ActionResult EliminarCategoria(FormCollection frm)
        {
            Int64 CategoriaID = Convert.ToInt64(frm["identificador"]);
            try
            {
                int i = rn_Categoria.Instancia.EliminarCategoria(CategoriaID);
                return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 3, Mensaje = "Se ha eliminado la categoría correctamente." });
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 2, Mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("SubMantenimientos", "IntranetInicio", new { Identificador = 1, Mensaje = e.Message });
            }
        }

        public ActionResult ModificarCategoria(Int16 CategoriaID,Int16 ?Identificador,String Mensaje)
        {
            ViewBag.identificador = Identificador;
            ViewBag.mensaje = Mensaje;
            ViewBag.ID = CategoriaID;
            List<Categoria> c = rn_Categoria.Instancia.BuscarCategoria(CategoriaID);
            return View(c);           
        }

        public ActionResult ActualizarCategoria(FormCollection frm, HttpPostedFileBase fotoi)
        {
            Int16 ID = Convert.ToInt16(frm["ID"]);
            try
            {
                
                String nombre = frm["nombre"].ToString();
                String foto = "";
                if (fotoi != null && fotoi.ContentLength > 0)
                {
                    foto = fotoi.FileName;
                }
                else
                {
                    foto = "";
                }
                int i = rn_Categoria.Instancia.ActualizarCategoria(ID, nombre, foto);
                if (i == 1)
                {
                    int f = GuardarFoto(fotoi, "I_Categoria");
                    if (f == 1)
                    {
                        return RedirectToAction("ModificarCategoria", "IntranetInicio", new { CategoriaID = ID, Identificador = 3, Mensaje = "Se ha agregado la categoría correctamente." });
                    }
                    else
                    {
                        return RedirectToAction("ModificarCategoria", "IntranetInicio", new { CategoriaID = ID, Identificador = 2, Mensaje = "La imagen no se ha copiado correctamente." });
                    }

                }
                else if (i == 2)
                {
                    return RedirectToAction("ModificarCategoria", "IntranetInicio", new { CategoriaID = ID, Identificador = 3, Mensaje = "Se ha agregado la categoría correctamente." });
                }
                else
                {
                    return RedirectToAction("ModificarCategoria", "IntranetInicio", new { CategoriaID = ID, Identificador = 2, Mensaje = "Parece que tenemos problemas para actualizar el perfil." });
                }     
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction("ModificarCategoria", "IntranetInicio", new { CategoriaID = ID, Identificador = 2, Mensaje = ae.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("ModificarCategoria", "IntranetInicio", new { CategoriaID = ID, Identificador = 1, Mensaje = e.Message });
            }
        }

        #endregion
    }
}
