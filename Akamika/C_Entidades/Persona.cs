using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Entidades
{
    public class Persona
    {
        public int PersonaID { get; set; }
        public string PersonaNombres { get; set; }
        public string PersonaApellidos { get; set; }
        public string PersonaSexo { get; set; }
        public string PersonaFoto { get; set; }
        public DateTime PersonaFechaNacimiento { get; set; }
        public string PersonaFechaRegistro { get; set; }
        public string PersonaEstado { get; set; }
        
    }
}
