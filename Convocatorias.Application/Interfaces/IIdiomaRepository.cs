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
        Task<string> InsertarAsync(IdiomaDTO entidad);
        Task<IEnumerable<IdiomaDTO>> ListarAsync(int iCodUsuario);
        Task<string> ActualizarAsync(IdiomaDTO entidad);
        Task<string> EliminarAsync(int iCodIdioma);
    }
}
