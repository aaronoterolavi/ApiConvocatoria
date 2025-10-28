using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IArchivoPostulacionRepository
    {
        Task<ArchivoPostulacionResponseDto> InsertarAsync(ArchivoPostulacionInsertarDto dto);
        Task<List<ArchivoPostulacionDto>> ListarAsync();
        //    Task<List<ArchivoPostulacionListadoDto>> ListarPorPostulacionAsync(int iCodPostulacion, int? iCodFormato);
        Task<IEnumerable<ArchivoPostulacionListadoDto>> ListarPorPostulacionAsync(
          
           int? iCodConvocatoria = null,
     int? iCodUsuario = null);
         
        }
}
