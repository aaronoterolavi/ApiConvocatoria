using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IExperienciaLaboralRepository
    {
        int Insertar(ExperienciaLaboralDto dto);
        void Actualizar(ExperienciaLaboralDto dto);
        void Eliminar(int id);
        List<ExperienciaLaboralDto> Listar();
        List<ExperienciaLaboralDto> ListarPorUsuario(int iCodUsuario);
    }
}
