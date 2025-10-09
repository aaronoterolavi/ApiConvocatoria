using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class CursoDiplomadoDto
    {
        public int iCodCursoDiplomado { get; set; }
        public int iCodPostulante { get; set; }
        public string vCurso { get; set; }
        public string vNombreInstitucion { get; set; }
        public int iHoras { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public bool bActivo { get; set; }
    }
}
