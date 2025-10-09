using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class ExperienciaLaboralService
    {
        private readonly IExperienciaLaboralRepository _repo;

        public ExperienciaLaboralService(IExperienciaLaboralRepository repo)
        {
            _repo = repo;
        }

        public Task<int> InsertarAsync(ExperienciaLaboralDto dto) => _repo.InsertarAsync(dto);
        public Task<List<ExperienciaLaboralDto>> ListarAsync() => _repo.ListarAsync();
        public Task<ExperienciaLaboralDto?> ObtenerPorIdAsync(int id) => _repo.ObtenerPorIdAsync(id);
        public Task<string> ActualizarAsync(ExperienciaLaboralDto dto) => _repo.ActualizarAsync(dto);
        public Task<string> EliminarAsync(int id) => _repo.EliminarAsync(id);
    }
}
