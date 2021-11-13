using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class Comentario
    {
        public int ComentarioID { get; set; }
        public Usuario Usuario { get; set; }
        public Curso Curso { get; set; }
        public String ComentarioDetalle { get; set; }
        public String FechaRegistroCM { get; set; }
        public Usuario UsuarioRegistroCM { get; set; }
        public String FechaModificacionCM { get; set; }
        public Usuario UsuarioModificacionCM { get; set; }
    }
}
