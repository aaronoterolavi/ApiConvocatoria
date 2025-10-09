using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class LoginResponseDto
    {
        public int IdUsuario { get; set; }
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public int TipoDocumento { get; set; } 
        public string NroDocumento {  get; set; } = string.Empty;
        public int CodRol { get; set; }
        public bool Activo { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}
