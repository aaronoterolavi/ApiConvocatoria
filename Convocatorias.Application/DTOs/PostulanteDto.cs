using System;

namespace Convocatorias.Application.DTOs
{
    public class PostulanteDto
    {
        public int CodPostulante { get; set; }
        public int CodUsuario { get; set; }
        public string? CodigoPostulacion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? CodSexo { get; set; }
        public int? CodEstadoCivil { get; set; }
        public string? CodDepartamento { get; set; }
        public string? CodProvincia { get; set; }
        public string? CodDistrito { get; set; }
        public string? Domicilio { get; set; }
        public string? Celular { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}
