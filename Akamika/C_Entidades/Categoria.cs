using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaEstado { get; set; }
        public string CategoriaImagen { get; set; }
        public string CategoriaIcono { get; set; }
        public string FechaRegistroC { get; set; }
        public Usuario UsuarioRegistroC { get; set; }
        public string FechaModificacionC { get; set; }
        public Usuario UsuarioModificacionC { get; set; }
    }
}
