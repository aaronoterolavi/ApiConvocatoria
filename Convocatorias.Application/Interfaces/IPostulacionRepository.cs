using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IPostulacionRepository
    {
        Task<(string Mensaje, int? iCodPostulacion)> InsertarAsync(int iCodUsuario, int iCodConvocatoria, int iCodUsuarioRegistra);
        Task<string> ActualizarAsync(int iCodPostulacion, int iCodUsuario, int iCodConvocatoria, int iCodUsuarioRegistra);
        Task<string> EliminarAsync(int iCodPostulacion);

        Task<List<PostulacionDto>> ListarAsync(int? iCodUsuario, int? iCodConvocatoria, bool soloActivos);

        Task<(List<PostulacionDto> Items, int TotalRegistros)> ListarPaginadoAsync(
            int? iCodPostulacion,
            int? iCodUsuario,
            int? iCodConvocatoria,
            int? iCodTipoConvocatoria,
            int? iCodUnidadZonal,
            string? vNumDocumento,
            string? vNombreCompleto,
            string? vTituloConvocatoria,
            string? vTipoConvocatoria,
            string? vUnidadZonal,
            DateTime? FechaPostulacionDesde,
            DateTime? FechaPostulacionHasta,
            bool soloActivos,
            int PageNumber,
            int PageSize
        );
    }
}
