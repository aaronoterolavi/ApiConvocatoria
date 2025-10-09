using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface ICursoDiplomadoRepository
    {
        Task<int> InsertAsync(CursoDiplomadoDto dto);
        Task<string> UpdateAsync(CursoDiplomadoDto dto);
        Task<string> DeleteAsync(int id);
        Task<CursoDiplomadoDto?> GetByIdAsync(int id);
        Task<List<CursoDiplomadoDto>> GetByPostulanteAsync(int iCodPostulante);
    }
}
