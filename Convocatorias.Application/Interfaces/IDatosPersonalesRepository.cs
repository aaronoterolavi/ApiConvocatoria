using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IDatosPersonalesRepository
    {
        Task<IEnumerable<DatosPersonalesDTO>> ListarAsync();
        Task<DatosPersonalesDTO?> ObtenerPorUsuarioAsync(int iCodUsuario);
        Task InsertarAsync(DatosPersonalesDTO datos);
        Task ActualizarAsync(DatosPersonalesDTO datos);
        Task EliminarAsync(int iCodDatosPersonales);
    }
}
