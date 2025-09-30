using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{

    public class ConvocatoriaInsertarResponseDto
    {
        // NuevoId viene del SP cuando es exitoso (SCOPE_IDENTITY)
        public int NuevoId { get; set; }

        // Mensaje que devuelve el SP (o del servicio)
        public string Mensaje { get; set; } = string.Empty;

        // Codigo: 1=ok, 0=error (según tu SP)
        public int Codigo { get; set; }
    }
}
