using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class DeclaracionJuradaPostulanteDTO
    {
        public int iCodDeclaracionJuradaPostulante { get; set; }
        public int iCodUsuario { get; set; }
        public bool bSinAntecedentesPenales { get; set; }
        public bool bSinProcesosJudiciales { get; set; }
        public bool bSinSancionesAdministrativas { get; set; }
        public bool bSinVinculoLaboralEstado { get; set; }
        public bool bAceptaBasesConcurso { get; set; }
        public int iCodUsuarioRegistra { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
