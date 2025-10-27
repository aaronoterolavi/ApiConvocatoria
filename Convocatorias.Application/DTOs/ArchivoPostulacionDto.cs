using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ArchivoPostulacionDto
    {
        public int iCodArchivo { get; set; }
        public int iCodPostulacion { get; set; }
        public int iCodFormato { get; set; }
        public string vRutaArchivo { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
    }
}
