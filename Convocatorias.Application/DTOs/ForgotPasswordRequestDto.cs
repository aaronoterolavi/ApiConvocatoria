using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ForgotPasswordRequestDto
    {
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
