using Convocatorias.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Convocatorias.Application.Interfaces
{
    public interface IPostulanteRepository
    {
        Task<List<PostulanteDto>> ListarAsync();
        Task<PostulanteDto?> ObtenerPorIdAsync(int id);
        Task<string> InsertarAsync(PostulanteDto postulante);
        Task<string> ActualizarAsync(PostulanteDto postulante);
        Task<string> EliminarAsync(int id);
    }
}
