using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IConvocatoriaEvaluadorRepository
    {
        Task<string> AsignarEvaluadorAConvocatoriaAsync(int iCodConvocatoria, int iCodUsuarioEvaluador, int iCodUsuarioRegistra);
        Task<string> DesactivarAsignacionEvaluadorAsync(int iCodConvocatoria, int iCodUsuarioEvaluador, int iCodUsuarioAccion);
        Task<List<ConvocatoriaEvaluadorDto>> ListarEvaluadoresPorConvocatoriaAsync(int iCodConvocatoria);
        Task<List<ConvocatoriaEvaluadorDto>> ListarConvocatoriasPorEvaluadorAsync(int iCodUsuarioEvaluador);
    }
}
