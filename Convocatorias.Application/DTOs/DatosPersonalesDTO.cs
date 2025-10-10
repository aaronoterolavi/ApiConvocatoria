using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class DatosPersonalesDTO
    {
        public int iCodDatosPersonales { get; set; }
        public int iCodUsuario { get; set; }
        public string vCodigoPostulacion { get; set; }
        public DateTime dFechaNacimiento { get; set; }
        public int iCodSexo { get; set; }
        public int iCodEstadoCivil { get; set; }
        public string vCodDepartamento { get; set; }
        public string vCodProvincia { get; set; }
        public string vCodDistrito { get; set; }
        public string vDomicilio { get; set; }
        public string vCelular { get; set; }
        public string vTelefono { get; set; }
        public string vCorreo { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public bool bActivo { get; set; }
    }
}
