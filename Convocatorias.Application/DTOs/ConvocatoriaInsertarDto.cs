using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class ConvocatoriaInsertarDto
    {
        [Required]
        [StringLength(250)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public int CodTipoConvocatoria { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        public int CodUnidadZonal { get; set; }

        public string? Requisitos { get; set; }

        [Required]
        public int CodUsuarioRegistra { get; set; }
    }
}
