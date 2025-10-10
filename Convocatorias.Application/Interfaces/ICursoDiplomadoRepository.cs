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
        Task InsertarAsync(CursoDiplomadoDTO entidad);
        Task<IEnumerable<CursoDiplomadoDTO>> ListarAsync(int iCodUsuario);
        Task ActualizarAsync(CursoDiplomadoDTO entidad);
        Task EliminarAsync(int iCodCursoDiplomado);
    }
}
