using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaFaseInsertarDto
    {
        public int CodConvocatoria { get; set; }
        public int CodEstado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CodUsuarioRegistra { get; set; }
    }
}
