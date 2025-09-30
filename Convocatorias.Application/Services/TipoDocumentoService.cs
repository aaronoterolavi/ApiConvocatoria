using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class TipoDocumentoService
    {
        private readonly ITipoDocumentoRepository _repo;

        public TipoDocumentoService(ITipoDocumentoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TipoDocumentoDto>> ListarTiposDocumentoAsync()
        {
            return await _repo.ListarAsync();
        }
    }
}
