using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IExperienciaLaboralRepository
    {
        Task<int> InsertarAsync(ExperienciaLaboralDto dto);
        Task<List<ExperienciaLaboralDto>> ListarAsync();
        Task<ExperienciaLaboralDto?> ObtenerPorIdAsync(int id);
        Task<string> ActualizarAsync(ExperienciaLaboralDto dto);
        Task<string> EliminarAsync(int id);
    }
}
