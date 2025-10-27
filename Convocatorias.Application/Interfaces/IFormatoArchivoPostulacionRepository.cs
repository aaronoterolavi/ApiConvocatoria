using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IFormatoArchivoPostulacionRepository
    {
        Task<FormatoArchivoPostulacionResponseDto> InsertarAsync(FormatoArchivoPostulacionInsertarDto dto);
        Task<FormatoArchivoPostulacionResponseDto> ActualizarAsync(FormatoArchivoPostulacionActualizarDto dto);
        Task<FormatoArchivoPostulacionResponseDto> EliminarAsync(int iCodFormato);
        Task<List<FormatoArchivoPostulacionDto>> ListarAsync();
    }
}
