using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class CursoRegistro
    {
        public int CursoRegistroID { get; set; }
        public Sesion Sesion { get; set; }
        public Usuario Usuario { get; set; }
        public String CursoRegistroEstado { get; set; }
        public String FechaRegistroCR { get; set; }
        public Usuario UsuarioRegistroCR { get; set; }
        public string FechaMoficacionCR { get; set; }
        public Usuario UsuarioModificacionCR { get; set; }
    }
}
