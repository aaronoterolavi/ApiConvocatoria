using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class BonificacionesAdicionalesService
    {
        private readonly IBonificacionesAdicionalesRepository _repo;

        public BonificacionesAdicionalesService(IBonificacionesAdicionalesRepository repo)
        {
            _repo = repo;
        }

        public Task<string> InsertarAsync(BonificacionesAdicionalesDTO dto) => _repo.InsertarAsync(dto);
        public Task<string> ActualizarAsync(BonificacionesAdicionalesDTO dto) => _repo.ActualizarAsync(dto);
        public Task<string> EliminarAsync(int id) => _repo.EliminarAsync(id);
        public Task<(List<BonificacionesAdicionalesDTO> lista, string mensaje)> ListarAsync(int? iCodUsuario) => _repo.ListarAsync(iCodUsuario);
    }
}
