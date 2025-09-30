using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class UnidadZonalService
    {
        private readonly IUnidadZonalRepository _repo;

        public UnidadZonalService(IUnidadZonalRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<UnidadZonalDto>> ListarUnidadesAsync()
        {
            return await _repo.ListarAsync();
        }
    }
}
