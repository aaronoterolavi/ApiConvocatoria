using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IOfimaticaNivelIntermedioRepository
    {
        int Insert(OfimaticaNivelIntermedioDTO dto);
        void Update(int iCodOfimaticaNivelIntermedio, bool bTieneConocimiento);
        void Delete(int iCodOfimaticaNivelIntermedio);
        OfimaticaNivelIntermedioDTO? GetById(int iCodOfimaticaNivelIntermedio);
        OfimaticaNivelIntermedioDTO? GetByPostulante(int iCodPostulante);
    }
}
