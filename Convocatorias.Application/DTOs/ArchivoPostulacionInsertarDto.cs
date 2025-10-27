using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ArchivoPostulacionInsertarDto
    {
        public int iCodPostulacion { get; set; }
        public int iCodFormato { get; set; }
        public string vRutaArchivo { get; set; }
        public int iCodUsuarioRegistra { get; set; }
    }
}
