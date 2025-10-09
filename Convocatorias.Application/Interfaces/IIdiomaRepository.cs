using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IIdiomaRepository
    {
        int Insert(IdiomaDTO dto);
        void Update(IdiomaDTO dto);
        void Delete(int iCodIdioma);
        IdiomaDTO? GetById(int iCodIdioma);
        List<IdiomaDTO> GetByPostulante(int iCodPostulante);
    }
}
