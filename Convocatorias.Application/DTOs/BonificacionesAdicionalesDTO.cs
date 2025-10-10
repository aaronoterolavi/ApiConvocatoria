using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class BonificacionesAdicionalesDTO
    {
        public int iCodBonificaciones { get; set; }
        public int iCodUsuario { get; set; }
        public bool bLicenciaFFAA { get; set; }
        public string vNroCarnetFFAA { get; set; } = string.Empty;
        public bool bDiscapacidad { get; set; }
        public string vNroCarnetDiscapacidad { get; set; } = string.Empty;
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
