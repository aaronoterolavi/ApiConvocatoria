using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IOfimaticaNivelIntermedioRepository
    {
        string Insertar(OfimaticaNivelIntermedioDTO dto);
        string Actualizar(OfimaticaNivelIntermedioDTO dto);
        string Eliminar(int iCodOfimaticaNivelIntermedio);
        (List<OfimaticaNivelIntermedioDTO> lista, string mensaje) Listar(int? iCodUsuario = null);
    }
}
