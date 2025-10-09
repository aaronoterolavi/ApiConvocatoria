using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;

namespace Convocatorias.Application.Services
{
    public class FormacionAcademicaService
    {
        private readonly IFormacionAcademicaRepository _repository;

        public FormacionAcademicaService(IFormacionAcademicaRepository repository)
        {
            _repository = repository;
        }

        public Task<int> InsertarAsync(FormacionAcademicaDto dto) => _repository.InsertarAsync(dto);

        public Task<IEnumerable<FormacionAcademicaDto>> ListarAsync() => _repository.ListarAsync();

        public Task<FormacionAcademicaDto?> ObtenerPorIdAsync(int id) => _repository.ObtenerPorIdAsync(id);

        public Task<string> ActualizarAsync(FormacionAcademicaDto dto) => _repository.ActualizarAsync(dto);

        public Task<string> EliminarAsync(int id) => _repository.EliminarAsync(id);
    }
}
