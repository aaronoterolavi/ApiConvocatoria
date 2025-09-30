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
        Task<IEnumerable<UsuarioListarDto>> ListarAsync(); // 👈 Nuevo para el endpoint Listar

        Task<int?> GetUserIdByEmailAsync(string correo);
        Task<bool> SavePasswordResetTokenAsync(int userId, string token, DateTime expiracion);
        Task<int?> GetUserIdByResetTokenAsync(string token); // devuelve userId si válido, null si no
        Task<bool> ResetPasswordByTokenAsync(string token, string hashedPassword);
    }
}
