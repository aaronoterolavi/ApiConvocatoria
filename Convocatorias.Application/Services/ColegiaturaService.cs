using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class ColegiaturaService
    {
        private readonly IColegiaturaRepository _repository;

        public ColegiaturaService(IColegiaturaRepository repository)
        {
            _repository = repository;
        }

        public Task<int> InsertarAsync(ColegiaturaDto dto) => _repository.InsertarAsync(dto);

        public Task<IEnumerable<ColegiaturaDto>> ListarAsync() => _repository.ListarAsync();

        public Task<ColegiaturaDto?> ObtenerPorIdAsync(int id) => _repository.ObtenerPorIdAsync(id);

        public Task<string> ActualizarAsync(ColegiaturaDto dto) => _repository.ActualizarAsync(dto);

        public Task<string> EliminarAsync(int id) => _repository.EliminarAsync(id);
    }
}
