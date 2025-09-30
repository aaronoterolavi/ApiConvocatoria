using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class EstadoService
    {
        private readonly IEstadoRepository _repo;

        public EstadoService(IEstadoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<EstadoDto>> ListarEstadosAsync()
        {
            return await _repo.ListarAsync();
        }
    }


}
