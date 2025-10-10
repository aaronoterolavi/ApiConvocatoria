using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class DeclaracionJuradaPostulanteService
    {
        private readonly IDeclaracionJuradaPostulanteRepository _repo;

        public DeclaracionJuradaPostulanteService(IDeclaracionJuradaPostulanteRepository repo)
        {
            _repo = repo;
        }

        public Task<string> InsertarAsync(DeclaracionJuradaPostulanteDTO dto) => _repo.InsertarAsync(dto);
        public Task<string> ActualizarAsync(DeclaracionJuradaPostulanteDTO dto) => _repo.ActualizarAsync(dto);
        public Task<string> EliminarAsync(int id) => _repo.EliminarAsync(id);
        public Task<List<DeclaracionJuradaPostulanteDTO>> ListarAsync() => _repo.ListarAsync();
    }
}
