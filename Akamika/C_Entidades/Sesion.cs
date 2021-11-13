using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class Sesion
    {
        public int SesionID { get; set; }
        public string SesionTitulo { get; set; }
        public string SesionDescripcion { get; set; }
        public string SesionVideo { get; set; }
        public string SesionEstado { get; set; }
        public Curso Curso { get; set; }
        public string FechaRegistroS { get; set; }
        public Usuario UsuarioRegistroS { get; set; }
        public string FechaModificacionS { get; set; }
        public Usuario UsuarioModificacion { get; set; }
    }
}
