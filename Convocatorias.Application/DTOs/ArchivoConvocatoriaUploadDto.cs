using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Convocatorias.Application.DTOs
{
    public class ArchivoConvocatoriaUploadDto
    {
        public IFormFile File { get; set; } = null!;
        public int CodFormato { get; set; }
    }
}
