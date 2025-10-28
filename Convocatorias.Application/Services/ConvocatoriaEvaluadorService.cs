using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class ConvocatoriaEvaluadorService
    {
        private readonly IConvocatoriaEvaluadorRepository _repo;

        public ConvocatoriaEvaluadorService(IConvocatoriaEvaluadorRepository repo)
        {
            _repo = repo;
        }

        public Task<string> AsignarEvaluadorAConvocatoriaAsync(int iCodConvocatoria, int iCodUsuarioEvaluador, int iCodUsuarioRegistra)
            => _repo.AsignarEvaluadorAConvocatoriaAsync(iCodConvocatoria, iCodUsuarioEvaluador, iCodUsuarioRegistra);

        public Task<string> DesactivarAsignacionEvaluadorAsync(int iCodConvocatoria, int iCodUsuarioEvaluador, int iCodUsuarioAccion)
            => _repo.DesactivarAsignacionEvaluadorAsync(iCodConvocatoria, iCodUsuarioEvaluador, iCodUsuarioAccion);

        public Task<List<ConvocatoriaEvaluadorDto>> ListarEvaluadoresPorConvocatoriaAsync(int iCodConvocatoria)
            => _repo.ListarEvaluadoresPorConvocatoriaAsync(iCodConvocatoria);

        public Task<List<ConvocatoriaEvaluadorDto>> ListarConvocatoriasPorEvaluadorAsync(int iCodUsuarioEvaluador)
            => _repo.ListarConvocatoriasPorEvaluadorAsync(iCodUsuarioEvaluador);
    }
}
