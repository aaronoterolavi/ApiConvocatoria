using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

 
namespace Convocatorias.Application.Interfaces
{
    public interface IFormacionAcademicaRepository
    {
        Task<IEnumerable<FormacionAcademicaDTO>> ListarAsync();
        Task<IEnumerable<FormacionAcademicaDTO>> ListarPorUsuarioAsync(int iCodUsuario);
        Task<int> InsertarAsync(FormacionAcademicaDTO dto);
        Task ActualizarAsync(FormacionAcademicaDTO dto);
        Task EliminarAsync(int iCodFormacionAcademica);
    }
}

