using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

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

        // ✅ Nuevo método para registrar varias fases
        public async Task<List<ConvocatoriaFaseResponseDto>> RegistrarVariasFasesAsync(
            List<ConvocatoriaFaseInsertarDto> fases)
        {
            var resultados = new List<ConvocatoriaFaseResponseDto>();

            foreach (var fase in fases)
            {
                var result = await _repo.InsertarAsync(fase);
                resultados.Add(result);
            }

            return resultados;
        }

        // ✅ Listar fases por convocatoria
        public async Task<IEnumerable<ConvocatoriaFaseDto>> ListarPorConvocatoriaAsync(int iCodConvocatoria)
        {
            return await _repo.ListarPorConvocatoriaAsync(iCodConvocatoria);
        }
    }
}
