using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ArchivoConvocatoriaDto
    {
        public int iCodAdjunto { get; set; }
        public string vRutaArchivo { get; set; } = string.Empty;
        public int iCodConvocatoria { get; set; }
        public int iCodFormato { get; set; }
        public string vDescFormato { get; set; } = string.Empty;
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }

        // ✅ Nuevo campo calculado para devolver URL accesible
        public string UrlArchivo { get; set; } = string.Empty;
        public string vNombreArchivo { get; set; } = string.Empty;


    }
}
