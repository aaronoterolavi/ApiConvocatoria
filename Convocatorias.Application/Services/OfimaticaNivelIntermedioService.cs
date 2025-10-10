using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class OfimaticaNivelIntermedioService
    {
        private readonly IOfimaticaNivelIntermedioRepository _repository;

        public OfimaticaNivelIntermedioService(IOfimaticaNivelIntermedioRepository repository)
        {
            _repository = repository;
        }

        public string Insertar(OfimaticaNivelIntermedioDTO dto) => _repository.Insertar(dto);
        public string Actualizar(OfimaticaNivelIntermedioDTO dto) => _repository.Actualizar(dto);
        public string Eliminar(int id) => _repository.Eliminar(id);
        public (List<OfimaticaNivelIntermedioDTO> lista, string mensaje) Listar(int? iCodUsuario = null) => _repository.Listar(iCodUsuario);
    }
}
