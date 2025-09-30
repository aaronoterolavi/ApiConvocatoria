using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaFaseResponseDto
    {
        public int NuevoId { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int Codigo { get; set; }   // 1 = OK, 0 = Error
    }
}
