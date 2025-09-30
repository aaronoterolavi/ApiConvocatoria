using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ArchivoConvocatoriaInsertarDto
    {
        public string RutaArchivo { get; set; } = string.Empty;
        public int CodConvocatoria { get; set; }
        public int CodFormato { get; set; }
        public int CodUsuarioRegistra { get; set; }
    }
}
