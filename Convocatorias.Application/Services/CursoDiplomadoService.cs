using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class CursoDiplomadoService
    {
        private readonly ICursoDiplomadoRepository _repository;

        public CursoDiplomadoService(ICursoDiplomadoRepository repository)
        {
            _repository = repository;
        }

        public Task<int> InsertAsync(CursoDiplomadoDto dto) => _repository.InsertAsync(dto);
        public Task<string> UpdateAsync(CursoDiplomadoDto dto) => _repository.UpdateAsync(dto);
        public Task<string> DeleteAsync(int id) => _repository.DeleteAsync(id);
        public Task<CursoDiplomadoDto?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<List<CursoDiplomadoDto>> GetByPostulanteAsync(int iCodPostulante) => _repository.GetByPostulanteAsync(iCodPostulante);
    }
}
