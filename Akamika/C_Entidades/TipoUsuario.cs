using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class TipoUsuario
    {
        public int TipoUsuarioID {get;set;}
        public String TipoUsuarioNombre {get;set;}
        public String TipoUsuarioEstado {get;set;}
        public String FechaRegistroTU {get;set;}
        public int UsuarioRegistroTU {get;set;}
        public String FechaModificacionTU {get;set;}
        public Usuario UsuarioModificacionTU { get; set; }
    }
}
