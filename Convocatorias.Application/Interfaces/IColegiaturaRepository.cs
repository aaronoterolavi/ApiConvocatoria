using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IColegiaturaRepository
    {
        Task<int> InsertarAsync(ColegiaturaDto dto);
        Task<IEnumerable<ColegiaturaDto>> ListarAsync();
        Task<ColegiaturaDto?> ObtenerPorIdAsync(int id);
        Task<string> ActualizarAsync(ColegiaturaDto dto);
        Task<string> EliminarAsync(int id);
    }
}
