using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaConFaseDto
    {
        public string iCodEstadoConvocatoria { get; set; } = string.Empty;
        public string vEstadoConvocatoria { get; set; } = string.Empty;
        public int iCodConvocatoria { get; set; }
        public string vTitulo { get; set; } = string.Empty;
        public int iCodTipoConvocatoria { get; set; }
        public string vTipoConvocatoria { get; set; } = string.Empty;
        public DateTime dtFechaInicio { get; set; }
        public DateTime dtFechaFin { get; set; }
        public string vRequisitos { get; set; } = string.Empty;
        public int iCodUnidadZonal { get; set; }
        public string vUnidadZonal { get; set; } = string.Empty;
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
        public int TotalRegistros { get; set; } // agregado para devolver total
    }
}
