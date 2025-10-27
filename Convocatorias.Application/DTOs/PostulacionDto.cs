using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class PostulacionDto
    {
        // Clave
        public int iCodPostulacion { get; set; }

        // Datos de usuario
        public int iCodUsuario { get; set; }
        public string? vNumDocumento { get; set; }
        public string? vNombreCompleto { get; set; }
        public string? vCorreoElectronico { get; set; }

        // Datos de convocatoria
        public int iCodConvocatoria { get; set; }
        public string? vTituloConvocatoria { get; set; }
        public int? iCodTipoConvocatoria { get; set; }
        public string? vTipoConvocatoria { get; set; }
        public int? iCodEstadoConvocatoria  { get; set; }
        public string? vEstadoConvocatoria { get; set; }
        public int? iCodUnidadZonal { get; set; }
        public string? vUnidadZonal { get; set; }

        // Fechas y otros
        public DateTime? dtFechaInicio { get; set; }
        public DateTime? dtFechaFin { get; set; }
        public string? vRequisitos { get; set; }

        public DateTime? dtFechaPostulacion { get; set; }
        public int? iCodUsuarioRegistra { get; set; }
        public bool bActivo { get; set; }
    }
}
