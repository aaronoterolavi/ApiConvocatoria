using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Convocatorias.Application.Services
{
    public class PostulanteService
    {
        private readonly IPostulanteRepository _repo;

        public PostulanteService(IPostulanteRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PostulanteDto>> ListarPostulantesAsync()
        {
            return await _repo.ListarAsync();
        }

        public async Task<PostulanteDto?> ObtenerPorIdAsync(int id)
        {
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<string> InsertarPostulanteAsync(PostulanteDto dto)
        {
            return await _repo.InsertarAsync(dto);
        }

        public async Task<string> ActualizarPostulanteAsync(PostulanteDto dto)
        {
            return await _repo.ActualizarAsync(dto);
        }

        public async Task<string> EliminarPostulanteAsync(int id)
        {
            return await _repo.EliminarAsync(id);
        }
    }
}
