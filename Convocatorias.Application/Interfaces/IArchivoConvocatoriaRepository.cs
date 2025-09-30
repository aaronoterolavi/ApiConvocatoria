using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IArchivoConvocatoriaRepository
    {
        Task<ArchivoConvocatoriaResponseDto> InsertarAsync(ArchivoConvocatoriaInsertarDto dto);

        Task<IEnumerable<ArchivoConvocatoriaDto>> ListarPorConvocatoriaAsync(int iCodConvocatoria, int? iCodFormato = null);


        Task<EliminarArchivoResponseDto> EliminarArchivoAsync(int idAdjunto);
    }
}
