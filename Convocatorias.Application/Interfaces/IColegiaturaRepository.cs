using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IColegiaturaRepository
    {
        int Insertar(ColegiaturaDto dto);
        void Actualizar(ColegiaturaDto dto);
        void Eliminar(int iCodColegiatura);
        List<ColegiaturaDto> Listar();
        List<ColegiaturaDto> ListarPorUsuario(int iCodUsuario);
    }
}
