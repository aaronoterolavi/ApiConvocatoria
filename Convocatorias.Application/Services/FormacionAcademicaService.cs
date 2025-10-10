using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class FormacionAcademicaService
    {
        private readonly IFormacionAcademicaRepository _repo;

        public FormacionAcademicaService(IFormacionAcademicaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FormacionAcademicaDTO>> ListarAsync() =>
            await _repo.ListarAsync();

        public async Task<IEnumerable<FormacionAcademicaDTO>> ListarPorUsuarioAsync(int iCodUsuario) =>
            await _repo.ListarPorUsuarioAsync(iCodUsuario);

        public async Task<int> InsertarAsync(FormacionAcademicaDTO dto) =>
            await _repo.InsertarAsync(dto);

        public async Task ActualizarAsync(FormacionAcademicaDTO dto) =>
            await _repo.ActualizarAsync(dto);

        public async Task EliminarAsync(int iCodFormacionAcademica) =>
            await _repo.EliminarAsync(iCodFormacionAcademica);
    }
}
