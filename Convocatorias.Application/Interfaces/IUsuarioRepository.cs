using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioInsertarResponseDto> InsertarAsync(UsuarioInsertarDto usuario);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
        Task<PagedResult<UsuarioListarDto>> ListarAsync(int pageNumber,int pageSize,  int? codRol = null, string? correo = null,  string? nombreCompleto = null); // 👈 Nuevo para el endpoint Listar
        Task<UsuarioEliminarDto> EliminarAsync(int idUsuario);
        Task<UsuarioActualizarDto> ActualizarAsync(
    int idUsuario,
    int tipoDocumento,
    string numDocumento,
    string apePaterno,
    string apeMaterno,
    string nombres,
    string correoElectronico,
    string contrasenia,
    int codRol,
    bool activo);
        Task<int?> GetUserIdByEmailAsync(string correo);
        Task<bool> SavePasswordResetTokenAsync(int userId, string token, DateTime expiracion);
        Task<int?> GetUserIdByResetTokenAsync(string token); // devuelve userId si válido, null si no
        Task<bool> ResetPasswordByTokenAsync(string token, string hashedPassword);
    }
}
