using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class IdiomaService
    {
        private readonly IIdiomaRepository _repository;

        public IdiomaService(IIdiomaRepository repository)
        {
            _repository = repository;
        }

        public int Insert(IdiomaDTO dto) => _repository.Insert(dto);
        public void Update(IdiomaDTO dto) => _repository.Update(dto);
        public void Delete(int id) => _repository.Delete(id);
        public IdiomaDTO? GetById(int id) => _repository.GetById(id);
        public List<IdiomaDTO> GetByPostulante(int idPostulante) => _repository.GetByPostulante(idPostulante);
    }
}
