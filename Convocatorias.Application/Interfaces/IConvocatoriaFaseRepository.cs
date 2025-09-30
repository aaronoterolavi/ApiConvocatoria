using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IConvocatoriaFaseRepository
    {
        Task<ConvocatoriaFaseResponseDto> InsertarAsync(ConvocatoriaFaseInsertarDto dto);
        Task<IEnumerable<ConvocatoriaFaseDto>> ListarPorConvocatoriaAsync(int iCodConvocatoria);
    }
}
