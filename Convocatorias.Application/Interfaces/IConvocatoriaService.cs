using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IConvocatoriaService
    {
        Task<ConvocatoriaInsertarResponseDto> CrearConvocatoriaAsync(ConvocatoriaInsertarDto dto);

        Task<ConvocatoriaPaginadaDto> ListarConvocatoriasPaginadoAsync(
            int pageNumber, int pageSize, DateTime? fechaInicio, DateTime? fechaFin, int? codTipoConvocatoria,bool? bActivo, string? buscar);

        Task<ResponseDto> ActualizarAsync(ConvocatoriaUpdateDto dto);

        Task<ResponseDto> EliminarAsync(int idConvocatoria);

        Task<List<ConvocatoriaConFaseDto>> ListarConvocatoriasConFasePaginado(
        int? iCodTipoConvocatoria,
        int? iCodUnidadZonal,
        DateTime? FechaInicio,
        DateTime? FechaFin,
        string? FiltroGeneral,
        int PageNumber,
        int PageSize);
    }
}
 
