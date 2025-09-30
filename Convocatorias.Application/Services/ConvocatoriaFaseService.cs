using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using System.Transactions;

namespace Convocatorias.Application.Services
{
    public class ConvocatoriaFaseService
    {
        private readonly IConvocatoriaFaseRepository _repo;

        public ConvocatoriaFaseService(IConvocatoriaFaseRepository repo)
        {
            _repo = repo;
        }

        public async Task<ConvocatoriaFaseResponseDto> RegistrarFaseAsync(ConvocatoriaFaseInsertarDto dto)
        {
            return await _repo.InsertarAsync(dto);
        }

        public async Task<List<ConvocatoriaFaseResponseDto>> RegistrarVariasFasesAsync(List<ConvocatoriaFaseInsertarDto> fases)
        {
            var resultados = new List<ConvocatoriaFaseResponseDto>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var fase in fases)
                {
                    var result = await _repo.InsertarAsync(fase);

                    if (result.Codigo != 1)
                        throw new Exception($"Error al registrar la fase con estado {fase.CodEstado}: {result.Mensaje}");

                    resultados.Add(result);
                }

                scope.Complete();
            }

            return resultados;
        }

        public async Task<IEnumerable<ConvocatoriaFaseDto>> ListarPorConvocatoriaAsync(int iCodConvocatoria)
        {
            return await _repo.ListarPorConvocatoriaAsync(iCodConvocatoria);
        }

        public async Task<EliminarFaseResponseDto> EliminarFaseAsync(int iCodFase)
        {
            if (iCodFase <= 0)
                return new EliminarFaseResponseDto
                {
                    IdEliminado = 0,
                    Mensaje = "Código de fase inválido",
                    Codigo = 0
                };

            return await _repo.EliminarAsync(iCodFase);
        }

        public async Task<ConvocatoriaFaseResponseDto> ActualizarFaseAsync(ConvocatoriaFaseActualizarDto dto)
        {
            return await _repo.ActualizarAsync(dto);
        }
    }
}
