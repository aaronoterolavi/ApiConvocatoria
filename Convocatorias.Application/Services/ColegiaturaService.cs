using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class ColegiaturaService
    {
        private readonly IColegiaturaRepository _repository;

        public ColegiaturaService(IColegiaturaRepository repository)
        {
            _repository = repository;
        }

        public int Insertar(ColegiaturaDto dto) => _repository.Insertar(dto);

        public void Actualizar(ColegiaturaDto dto) => _repository.Actualizar(dto);

        public void Eliminar(int id) => _repository.Eliminar(id);

        public List<ColegiaturaDto> Listar() => _repository.Listar();

        public List<ColegiaturaDto> ListarPorUsuario(int iCodUsuario) => _repository.ListarPorUsuario(iCodUsuario);
    }
}
