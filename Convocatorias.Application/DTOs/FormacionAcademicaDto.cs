using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class FormacionAcademicaDto
    {
        public int iCodFormacionAcademica { get; set; }
        public int iCodPostulante { get; set; }
        public int iCodNivelAcademico { get; set; }
        public string vInstitucion { get; set; } = string.Empty;
        public string vProfesion { get; set; } = string.Empty;
        public DateTime dFechaEgreso { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
    }
}
