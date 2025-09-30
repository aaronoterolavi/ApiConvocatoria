using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class TipoConvocatoriaService
    {
        private readonly ITipoConvocatoriaRepository _repo;

        public TipoConvocatoriaService(ITipoConvocatoriaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TipoConvocatoriaDto>> ListarTiposAsync()
        {
            return await _repo.ListarAsync();
        }
    }
}
