using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class IdiomaService
    {
        private readonly IIdiomaRepository _repo;

        public IdiomaService(IIdiomaRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> InsertarAsync(IdiomaDTO dto) => await _repo.InsertarAsync(dto);
        public async Task<IEnumerable<IdiomaDTO>> ListarAsync(int iCodUsuario) => await _repo.ListarAsync(iCodUsuario);
        public async Task<string> ActualizarAsync(IdiomaDTO dto) => await _repo.ActualizarAsync(dto);
        public async Task<string> EliminarAsync(int iCodIdioma) => await _repo.EliminarAsync(iCodIdioma);
    }
}
