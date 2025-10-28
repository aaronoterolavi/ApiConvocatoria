using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaEvaluadorDto
    {
        public int iCodConvocatoriaEvaluador { get; set; }
        public int iCodConvocatoria { get; set; }
        public int iCodUsuarioEvaluador { get; set; }
        public string? vNombres { get; set; }
        public string? vApePaterno { get; set; }
        public string? vApeMaterno { get; set; }
        public string? vCorreoElectronico { get; set; }
        public int iCodRol { get; set; }
        public bool bActivo { get; set; }
        public DateTime dtFechaRegistro { get; set; }

        // Campos adicionales para ListarConvocatoriasPorEvaluador
        public string? vTitulo { get; set; }
        public int? iCodTipoConvocatoria { get; set; }
        public DateTime? dtFechaInicio { get; set; }
        public DateTime? dtFechaFin { get; set; }
        public string? vRequisitos { get; set; }
        public int? iCodUnidadZonal { get; set; }
        public bool? bActivoConvocatoria { get; set; }
        public DateTime? dtFechaAsignacion { get; set; }
    }
}
