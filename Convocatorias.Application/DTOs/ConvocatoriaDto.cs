using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaDto
    {
        public int iCodConvocatoria { get; set; }
        public string vTitulo { get; set; } = string.Empty;
        public int iCodTipoDocumento { get; set; }
        public string vDescripcionConvocatoria { get; set; } = string.Empty;
        public DateTime dtFechaInicio { get; set; }
        public DateTime dtFechaFin { get; set; }
    
        public string vRequisitos { get; set; } = string.Empty;
        public int iCodUnidadZonal { get; set; }
        public string vUnidadZonal { get; set; } = string.Empty;
        public bool bActivo { get; set; }
    }
}
