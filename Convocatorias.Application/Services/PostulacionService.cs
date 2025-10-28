using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class PostulacionService
    {
        private readonly IPostulacionRepository _repo;

        public PostulacionService(IPostulacionRepository repo)
        {
            _repo = repo;
        }

        public Task<(string Mensaje,int? iCodPostulacion)> InsertarAsync(int iCodUsuario, int iCodConvocatoria, int iCodUsuarioRegistra)
            => _repo.InsertarAsync(iCodUsuario, iCodConvocatoria, iCodUsuarioRegistra);

        public Task<string> ActualizarAsync(int iCodPostulacion, int iCodUsuario, int iCodConvocatoria, int iCodUsuarioRegistra)
            => _repo.ActualizarAsync(iCodPostulacion, iCodUsuario, iCodConvocatoria, iCodUsuarioRegistra);

        public Task<string> EliminarAsync(int iCodPostulacion)
            => _repo.EliminarAsync(iCodPostulacion);

        public Task<List<PostulacionDto>> ListarAsync(int? iCodUsuario, int? iCodConvocatoria, bool soloActivos)
            => _repo.ListarAsync(iCodUsuario, iCodConvocatoria, soloActivos);

        public Task<(List<PostulacionDto> Items, int TotalRegistros)> ListarPaginadoAsync(
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
            int PageSize)
            => _repo.ListarPaginadoAsync(
                iCodPostulacion, iCodUsuario, iCodConvocatoria, iCodTipoConvocatoria, iCodUnidadZonal,
                vNumDocumento, vNombreCompleto, vTituloConvocatoria, vTipoConvocatoria, vUnidadZonal,
                FechaPostulacionDesde, FechaPostulacionHasta, soloActivos, PageNumber, PageSize);
    }
}
