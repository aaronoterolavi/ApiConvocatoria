using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaPaginadaDto
    {
        public List<ConvocatoriaDto> Items { get; set; } = new();
        public int TotalRecords { get; set; }
    }
}
