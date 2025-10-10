using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IBonificacionesAdicionalesRepository
    {
        Task<string> InsertarAsync(BonificacionesAdicionalesDTO dto);
        Task<string> ActualizarAsync(BonificacionesAdicionalesDTO dto);
        Task<string> EliminarAsync(int iCodBonificaciones);
        Task<(List<BonificacionesAdicionalesDTO> lista, string mensaje)> ListarAsync(int? iCodUsuario);
    }
}
