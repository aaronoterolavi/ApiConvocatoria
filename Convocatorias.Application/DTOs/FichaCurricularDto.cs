using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.DTOs
{
    public class FichaCurricularDto
    {
        public DatosPersonalesDto DatosPersonales { get; set; } = new();
        public List<FormacionAcademicaDto> Formaciones { get; set; } = new();
        public List<CursoDiplomadoDto> Cursos { get; set; } = new();
        public List<ExperienciaLaboral2Dto> Experiencias { get; set; } = new();
        public List<IdiomaDto> Idiomas { get; set; } = new();
        public string ConocimientoOfimatica { get; set; } = string.Empty;
        public DeclaracionJuradaDto DeclaracionJurada { get; set; } = new();
    }

    public class DatosPersonalesDto
    {
        public string vNumDocumento { get; set; } = string.Empty;
        public string vApePaterno { get; set; } = string.Empty;
        public string vApeMaterno { get; set; } = string.Empty;
        public string vNombres { get; set; } = string.Empty;
        public string vCorreoElectronico { get; set; } = string.Empty;
        public string vCodigoPostulacion { get; set; } = string.Empty;
        public DateTime? dFechaNacimiento { get; set; }
        public string vSexo { get; set; } = string.Empty;
        public string vEstadoCivil { get; set; } = string.Empty;
        public string vNomDepartamento { get; set; } = string.Empty;
        public string vNomProvincia { get; set; } = string.Empty;
        public string vNomDistrito { get; set; } = string.Empty;
        public string vDomicilio { get; set; } = string.Empty;
        public string vCelular { get; set; } = string.Empty;
        public string vTelefono { get; set; } = string.Empty;
        public string vNroColegiatura { get; set; } = string.Empty;
        public string vHabilitado { get; set; } = string.Empty;
        public string vLicenciaFFAA { get; set; } = string.Empty;
        public string vNroCarnetFFAA { get; set; } = string.Empty;
        public string vDiscapacidad { get; set; } = string.Empty;
        public string vNroCarnetDiscapacidad { get; set; } = string.Empty;
    }

    public class FormacionAcademicaDto
    {
        public string vNivelAcademico { get; set; } = string.Empty;
        public string vInstitucion { get; set; } = string.Empty;
        public string vProfesion { get; set; } = string.Empty;
        public DateTime? dFechaEgreso { get; set; }
    }

    public class CursoDiplomadoDto
    {
        public string vCurso { get; set; } = string.Empty;
        public string vNombreInstitucion { get; set; } = string.Empty;
        public int iHoras { get; set; }
    }

    public class ExperienciaLaboral2Dto
    {
        public string vEntidad { get; set; } = string.Empty;
        public string vUnidadOrganica { get; set; } = string.Empty;
        public string vCargo { get; set; } = string.Empty;
        public string cSector { get; set; } = string.Empty;
        public DateTime? dFechaInicio { get; set; }
        public DateTime? dFechaFin { get; set; }
        public string vFunciones { get; set; } = string.Empty;
        public string vActAgricolas { get; set; } = string.Empty;
        public string vActAgropecuarias { get; set; } = string.Empty;
        public string vTemasSanitarios { get; set; } = string.Empty;
        public string vAccesoMercadosExternos { get; set; } = string.Empty;
    }

    public class IdiomaDto
    {
        public string vIdioma { get; set; } = string.Empty;
        public string vInstitucion { get; set; } = string.Empty;
        public string vNivelAlcanzado { get; set; } = string.Empty;
    }

    public class DeclaracionJuradaDto
    {
        public string vSinAntecedentesPenales { get; set; } = string.Empty;
        public string vSinProcesosJudiciales { get; set; } = string.Empty;
        public string vSinSancionesAdministrativas { get; set; } = string.Empty;
        public string vSinVinculoLaboralEstado { get; set; } = string.Empty;
        public string vAceptaBasesConcurso { get; set; } = string.Empty;
    }
}
