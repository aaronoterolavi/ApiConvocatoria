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

        public int Insert(OfimaticaNivelIntermedioDTO dto)
        {
            return _repository.Insert(dto);
        }

        public void Update(int iCodOfimaticaNivelIntermedio, bool bTieneConocimiento)
        {
            _repository.Update(iCodOfimaticaNivelIntermedio, bTieneConocimiento);
        }

        public void Delete(int iCodOfimaticaNivelIntermedio)
        {
            _repository.Delete(iCodOfimaticaNivelIntermedio);
        }

        public OfimaticaNivelIntermedioDTO? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public OfimaticaNivelIntermedioDTO? GetByPostulante(int iCodPostulante)
        {
            return _repository.GetByPostulante(iCodPostulante);
        }
    }
}
