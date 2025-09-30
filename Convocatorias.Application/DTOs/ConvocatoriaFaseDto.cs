using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaFaseDto
    {
        public int iCodFase { get; set; }
        public int iCodConvocatoria { get; set; }
        public int iCodEstado { get; set; }
        public string vDescripcionEstado { get; set; } = string.Empty;
        public DateTime dtFechaInicio { get; set; }
        public DateTime dtFechaFin { get; set; }
        public bool bActivo { get; set; }
    }
}
