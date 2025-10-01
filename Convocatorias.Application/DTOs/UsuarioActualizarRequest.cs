using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class UsuarioActualizarRequest
    {
        public int TipoDocumento { get; set; }
        public string NumDocumento { get; set; } = string.Empty;
        public string ApePaterno { get; set; } = string.Empty;
        public string ApeMaterno { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public int CodRol { get; set; }
        public bool Activo { get; set; }
    }
}
