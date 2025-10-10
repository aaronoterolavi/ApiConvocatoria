using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IDeclaracionJuradaPostulanteRepository
    {
        Task<string> InsertarAsync(DeclaracionJuradaPostulanteDTO dto);
        Task<string> ActualizarAsync(DeclaracionJuradaPostulanteDTO dto);
        Task<string> EliminarAsync(int iCodDeclaracionJuradaPostulante);
        Task<List<DeclaracionJuradaPostulanteDTO>> ListarAsync();
    }
}
