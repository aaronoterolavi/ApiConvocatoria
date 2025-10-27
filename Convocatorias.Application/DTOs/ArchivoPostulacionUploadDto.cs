using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Convocatorias.Application.DTOs
{
    public class ArchivoPostulacionUploadDto
    {
        public IFormFile File { get; set; }
        public int iCodFormato { get; set; }
    }
}
