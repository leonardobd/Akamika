using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string CursoNombre { get; set; }
        public string CursoEstado { get; set; }
        public string CursoImagen { get; set; }
        public string CursoDescripción { get; set; }
        public string CursoFechaPublicacion { get; set; }
        public Categoria Categoria { get; set; }
        public string FechaRegistroCU { get; set; }
        public Usuario UsuarioRegistroCU { get; set; }
        public string FechaMoficacionCU { get; set; }
        public Usuario UsuarioModificacionCU { get; set; }
    }
}
