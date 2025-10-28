using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ArchivoPostulacionListadoDto
    {
        public int iCodArchivo { get; set; }
        public int iCodPostulacion { get; set; }
        public int iCodConvocatoria { get; set; }
        public int iCodUsuario { get; set; }
        public int iCodFormato { get; set; }
        public string vDescFormato { get; set; }
        public string vRutaArchivo { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }

        public string vNombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
    }
}
