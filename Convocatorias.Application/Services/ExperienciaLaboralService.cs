using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class ExperienciaLaboralService
    {
        private readonly IExperienciaLaboralRepository _repository;

        public ExperienciaLaboralService(IExperienciaLaboralRepository repository)
        {
            _repository = repository;
        }

        public int Insertar(ExperienciaLaboralDto dto) => _repository.Insertar(dto);
        public void Actualizar(ExperienciaLaboralDto dto) => _repository.Actualizar(dto);
        public void Eliminar(int id) => _repository.Eliminar(id);
        public List<ExperienciaLaboralDto> Listar() => _repository.Listar();
        public List<ExperienciaLaboralDto> ListarPorUsuario(int iCodUsuario) => _repository.ListarPorUsuario(iCodUsuario);
    }
}
