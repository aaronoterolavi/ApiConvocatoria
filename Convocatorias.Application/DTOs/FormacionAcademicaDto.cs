using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class FormacionAcademicaDTO
    {
        public int iCodFormacionAcademica { get; set; }
        public int iCodUsuario { get; set; }
        public int iCodNivelAcademico { get; set; }
        public string vInstitucion { get; set; }
        public string vProfesion { get; set; }
        public DateTime dFechaEgreso { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
    }
}
