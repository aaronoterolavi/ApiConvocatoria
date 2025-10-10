using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class DatosPersonalesService
    {
        private readonly IDatosPersonalesRepository _repo;

        public DatosPersonalesService(IDatosPersonalesRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DatosPersonalesDTO>> ListarAsync() =>
            await _repo.ListarAsync();

        public async Task<DatosPersonalesDTO?> ObtenerPorUsuarioAsync(int iCodUsuario) =>
            await _repo.ObtenerPorUsuarioAsync(iCodUsuario);

        public async Task InsertarAsync(DatosPersonalesDTO datos) =>
            await _repo.InsertarAsync(datos);

        public async Task ActualizarAsync(DatosPersonalesDTO datos) =>
            await _repo.ActualizarAsync(datos);

        public async Task EliminarAsync(int iCodDatosPersonales) =>
            await _repo.EliminarAsync(iCodDatosPersonales);
    }
}
