using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaUpdateDto
    {
        public int iCodConvocatoria { get; set; }
        public string vTitulo { get; set; }
        public int iCodTipoConvocatoria { get; set; }
        public DateTime dtFechaInicio { get; set; }
        public DateTime dtFechaFin { get; set; }
        public string vRequisitos { get; set; }
        public int iCodUnidadZonal { get; set; }
        public int iCodUsuarioRegistra { get; set; }
    }
}
