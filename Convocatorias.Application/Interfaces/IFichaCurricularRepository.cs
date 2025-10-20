using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IFichaCurricularRepository
    {
        Task<FichaCurricularDto> ObtenerFichaCurricularAsync(int iCodUsuario);
    }
}
