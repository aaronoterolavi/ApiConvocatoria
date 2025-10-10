using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ExperienciaLaboralDto
    {
        public int iCodExperienciaLaboral { get; set; }
        public int iCodUsuario { get; set; }
        public string vEntidad { get; set; } = string.Empty;
        public string? vUnidadOrganica { get; set; }
        public string vCargo { get; set; } = string.Empty;
        public string? cSector { get; set; }
        public DateTime dFechaInicio { get; set; }
        public DateTime? dFechaFin { get; set; }
        public string? vFunciones { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
    }
}
