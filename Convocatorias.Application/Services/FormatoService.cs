using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class FormatoService
    {
        private readonly IFormatoRepository _repo;

        public FormatoService(IFormatoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<FormatoDto>> ListarFormatosAsync()
        {
            return await _repo.ListarAsync();
        }
    }
}
