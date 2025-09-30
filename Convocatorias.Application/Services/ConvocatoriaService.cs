using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class ConvocatoriaService : IConvocatoriaService
    {
        private readonly IConvocatoriaRepository _repo;

        public ConvocatoriaService(IConvocatoriaRepository repo)
        {
            _repo = repo;
        }

        public async Task<ConvocatoriaInsertarResponseDto> CrearConvocatoriaAsync(ConvocatoriaInsertarDto dto)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                return new ConvocatoriaInsertarResponseDto { Codigo = 0, Mensaje = "El título es obligatorio." };

            if (dto.Titulo.Length > 250)
                return new ConvocatoriaInsertarResponseDto { Codigo = 0, Mensaje = "El título no puede tener más de 250 caracteres." };

            if (dto.FechaFin < dto.FechaInicio)
                return new ConvocatoriaInsertarResponseDto { Codigo = 0, Mensaje = "La fecha de fin no puede ser anterior a la fecha de inicio." };

            return await _repo.InsertarAsync(dto);
        }

        public async Task<ConvocatoriaPaginadaDto> ListarConvocatoriasPaginadoAsync(
            int pageNumber, int pageSize, DateTime? fechaInicio, DateTime? fechaFin, int? codTipoConvocatoria,bool? bActivo, string? buscar)
        {
            return await _repo.ListarConvocatoriasPaginadoAsync(pageNumber, pageSize, fechaInicio, fechaFin, codTipoConvocatoria,bActivo, buscar);
        }

        public async Task<ResponseDto> ActualizarAsync(ConvocatoriaUpdateDto dto)
        {
            return await _repo.ActualizarAsync(dto);
        }

        public async Task<ResponseDto> EliminarAsync(int idConvocatoria)
        {
            return await _repo.EliminarAsync(idConvocatoria);
        }
    }
}
