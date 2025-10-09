using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class IdiomaDTO
    {
        public int iCodIdioma { get; set; }
        public int iCodPostulante { get; set; }
        public string vIdioma { get; set; } = string.Empty;
        public string vInstitucion { get; set; } = string.Empty;
        public string vNivelAlcanzado { get; set; } = string.Empty;
        public DateTime dtFechaRegistro { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public bool bActivo { get; set; }
    }
}
