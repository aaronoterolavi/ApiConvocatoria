using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ColegiaturaDto
    {
        public int iCodColegiatura { get; set; }
        public int iCodPostulante { get; set; }
        public int iCodColegioProfesional { get; set; }
        public string vNroColegiatura { get; set; } = string.Empty;
        public bool bHabilitado { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
    }
}
