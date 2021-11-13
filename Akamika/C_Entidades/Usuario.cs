using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string UsuarioCorreo { get; set; }
        public string UsuarioPassw { get; set; }
        public TipoUsuario UsuarioTipo { get; set; }
        public Persona Persona { get; set; }
        public string UsuarioEstado { get; set; }
    }
}
