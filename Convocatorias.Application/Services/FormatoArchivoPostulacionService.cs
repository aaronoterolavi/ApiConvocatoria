using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class FormatoArchivoPostulacionService
    {
        private readonly IFormatoArchivoPostulacionRepository _repo;

        public FormatoArchivoPostulacionService(IFormatoArchivoPostulacionRepository repo)
        {
            _repo = repo;
        }

        public async Task<FormatoArchivoPostulacionResponseDto> InsertarAsync(FormatoArchivoPostulacionInsertarDto dto)
        {
            return await _repo.InsertarAsync(dto);
        }

        public async Task<FormatoArchivoPostulacionResponseDto> ActualizarAsync(FormatoArchivoPostulacionActualizarDto dto)
        {
            return await _repo.ActualizarAsync(dto);
        }

        public async Task<FormatoArchivoPostulacionResponseDto> EliminarAsync(int iCodFormato)
        {
            return await _repo.EliminarAsync(iCodFormato);
        }

        public async Task<List<FormatoArchivoPostulacionDto>> ListarAsync()
        {
            return await _repo.ListarAsync();
        }
    }
}
