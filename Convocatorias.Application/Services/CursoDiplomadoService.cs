using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class CursoDiplomadoService
    {
        private readonly ICursoDiplomadoRepository _repo;

        public CursoDiplomadoService(ICursoDiplomadoRepository repo)
        {
            _repo = repo;
        }

        public async Task InsertarAsync(CursoDiplomadoDTO dto) => await _repo.InsertarAsync(dto);
        public async Task<IEnumerable<CursoDiplomadoDTO>> ListarAsync(int iCodUsuario) => await _repo.ListarAsync(iCodUsuario);
        public async Task ActualizarAsync(CursoDiplomadoDTO dto) => await _repo.ActualizarAsync(dto);
        public async Task EliminarAsync(int iCodCursoDiplomado) => await _repo.EliminarAsync(iCodCursoDiplomado);
    }
}
