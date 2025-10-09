using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IFormacionAcademicaRepository
    {
        Task<int> InsertarAsync(FormacionAcademicaDto dto);
        Task<IEnumerable<FormacionAcademicaDto>> ListarAsync();
        Task<FormacionAcademicaDto?> ObtenerPorIdAsync(int id);
        Task<string> ActualizarAsync(FormacionAcademicaDto dto);
        Task<string> EliminarAsync(int id);
    }
}
