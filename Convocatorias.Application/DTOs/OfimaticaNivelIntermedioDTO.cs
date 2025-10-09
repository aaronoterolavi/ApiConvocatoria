using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class OfimaticaNivelIntermedioDTO
    {
        public int iCodOfimaticaNivelIntermedio { get; set; }
        public int iCodPostulante { get; set; }
        public bool bTieneConocimiento { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public bool bActivo { get; set; }
    }
}
