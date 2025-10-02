using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Convocatorias.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Convocatorias.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        private readonly PasswordHasher<object> _passwordHasher;
        private readonly IConfiguration _config;

        public UsuarioRepository(string connectionString, IConfiguration config)
        {
            _connectionString = connectionString;
            _passwordHasher = new PasswordHasher<object>();
            _config = config;
        }

        // Insertar usuario con password hasheado
        public async Task<UsuarioInsertarResponseDto> InsertarAsync(UsuarioInsertarDto usuario)
        {
            string hashedPassword = _passwordHasher.HashPassword(null, usuario.Contrasenia);

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_Usuarios_Insertar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@iTipoDocumento", usuario.TipoDocumento);
            cmd.Parameters.AddWithValue("@vNumDocumento", usuario.NumDocumento);
            cmd.Parameters.AddWithValue("@vApePaterno", usuario.ApePaterno);
            cmd.Parameters.AddWithValue("@vApeMaterno", usuario.ApeMaterno);
            cmd.Parameters.AddWithValue("@vNombres", usuario.Nombres);
            cmd.Parameters.AddWithValue("@vCorreoElectronico", usuario.CorreoElectronico);
            cmd.Parameters.AddWithValue("@vContrasenia", hashedPassword);
            cmd.Parameters.AddWithValue("@iCodRol", usuario.CodRol);
            cmd.Parameters.AddWithValue("@bActivo", usuario.Activo);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new UsuarioInsertarResponseDto
                {
                    NuevoId = reader.GetInt32(reader.GetOrdinal("NuevoId")),
                    Mensaje = reader.GetString(reader.GetOrdinal("Mensaje")),
                    Codigo = reader.GetInt32(reader.GetOrdinal("Codigo"))
                };
            }

            return new UsuarioInsertarResponseDto
            {
                NuevoId = 0,
                Mensaje = "No se pudo registrar el usuario",
                Codigo = 0
            };
        }

        // Validación de password
        public bool VerificarPassword(string hashedPassword, string passwordIngresada)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, passwordIngresada);
            return result == PasswordVerificationResult.Success;
        }

        // Login con JWT
        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_Usuarios_Login", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@vCorreoElectronico", request.CorreoElectronico);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            string passwordHash = reader["PasswordHash"].ToString() ?? "";
            var result = _passwordHasher.VerifyHashedPassword(null, passwordHash, request.Contrasenia);

            if (result != PasswordVerificationResult.Success)
                return null;

            int codRol = Convert.ToInt32(reader["iCodRol"]);

            // ✅ Generamos token con claim "CodRol"
            string token = GenerarToken(
                correo: reader["vCorreoElectronico"].ToString() ?? "",
                codUsuario: Convert.ToInt32(reader["iCodUsuario"]),
                codRol: codRol
            );

            return new LoginResponseDto
            {
                IdUsuario = Convert.ToInt32(reader["iCodUsuario"]),
                CorreoElectronico = reader["vCorreoElectronico"].ToString() ?? "",
                Nombres = reader["vNombres"].ToString() ?? "",
                CodRol = codRol,
                Activo = Convert.ToBoolean(reader["bActivo"]),
                Token = token,
                Mensaje = "Login exitoso"
            };
        }

        // Listar usuarios (solo se llama si rol=3 en el controlador)
        public async Task<PagedResult<UsuarioListarDto>> ListarAsync(
     int pageNumber,
     int pageSize,
     int? codRol = null,
     string? correo = null,
     string? nombreCompleto = null)
        {
            var usuarios = new List<UsuarioListarDto>();
            int totalRegistros = 0;

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_Usuarios_Listar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@iCodRol", (object?)codRol ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vCorreo", (object?)correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vNombreCompleto", (object?)nombreCompleto ?? DBNull.Value);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            // --- Primer resultset: lista de usuarios ---
            while (await reader.ReadAsync())
            {
                usuarios.Add(new UsuarioListarDto
                {
                    IdUsuario = reader.GetInt32(reader.GetOrdinal("iCodUsuario")),
                    TipoDocumento = reader.GetInt32(reader.GetOrdinal("iTipoDocumento")),
                    NumDocumento = reader.GetString(reader.GetOrdinal("vNumDocumento")),
                    ApePaterno = reader.GetString(reader.GetOrdinal("vApePaterno")),
                    ApeMaterno = reader.GetString(reader.GetOrdinal("vApeMaterno")),
                    Nombres = reader.GetString(reader.GetOrdinal("vNombres")),
                    CorreoElectronico = reader.GetString(reader.GetOrdinal("vCorreoElectronico")),
                    CodRol = reader.GetInt32(reader.GetOrdinal("iCodRol")),
                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                });
            }

            // --- Segundo resultset: total de registros ---
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                totalRegistros = reader.GetInt32(reader.GetOrdinal("TotalRegistros"));
            }

            return new PagedResult<UsuarioListarDto>
            {
                Items = usuarios,
                TotalRegistros = totalRegistros
            };
        }


        // Generar JWT
        private string GenerarToken(string correo, int codUsuario, int codRol)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, correo),
        new Claim("IdUsuario", codUsuario.ToString()),

        // 👇 Aquí usamos el parámetro codRol en vez de usuario.CodRol
        new Claim(ClaimTypes.Role, codRol.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // Obtener usuario por correo (solo id y nombre)
        public async Task<int?> GetUserIdByEmailAsync(string correo)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT iCodUsuario FROM T_Usuarios WHERE vCorreoElectronico = @correo AND bActivo = 1", conn);
            cmd.Parameters.AddWithValue("@correo", correo);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return result == null || result == DBNull.Value ? (int?)null : Convert.ToInt32(result);
        }

        // Guardar token
        public async Task<bool> SavePasswordResetTokenAsync(int userId, string token, DateTime expiracion)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_PasswordReset_InsertToken", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@iCodUsuario", userId);
            cmd.Parameters.AddWithValue("@vToken", token);
            cmd.Parameters.AddWithValue("@dtExpiracion", expiracion);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var codigo = reader["Codigo"] != DBNull.Value ? Convert.ToInt32(reader["Codigo"]) : 0;
                return codigo == 1;
            }
            return false;
        }

        // Validar token -> devuelve userId si válido
        public async Task<int?> GetUserIdByResetTokenAsync(string token)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_PasswordReset_ValidateToken", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@vToken", token);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            var usado = reader["bUsado"] != DBNull.Value && Convert.ToBoolean(reader["bUsado"]);
            var expiracion = reader["dtExpiracion"] != DBNull.Value ? Convert.ToDateTime(reader["dtExpiracion"]) : DateTime.MinValue;
            var usuarioId = reader["iCodUsuario"] != DBNull.Value ? Convert.ToInt32(reader["iCodUsuario"]) : (int?)null;

            if (usuarioId == null) return null;
            if (usado) return null;
            if (expiracion < DateTime.UtcNow) return null;

            return usuarioId;
        }

        // Resetear contraseña (llama al SP que hace la actualización y marca token como usado)
        public async Task<bool> ResetPasswordByTokenAsync(string token, string hashedPassword)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_Usuarios_ResetPasswordByToken", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@vToken", token);
            cmd.Parameters.AddWithValue("@vContrasenia", hashedPassword);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var codigo = reader["Codigo"] != DBNull.Value ? Convert.ToInt32(reader["Codigo"]) : 0;
                return codigo == 1;
            }
            return false;
        }

        public async Task<UsuarioEliminarDto> EliminarAsync(int idUsuario)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_Usuarios_Eliminar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@iCodUsuario", idUsuario);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new UsuarioEliminarDto
                {
                    Mensaje = reader["Mensaje"]?.ToString() ?? string.Empty,
                    Codigo = reader["Codigo"] != DBNull.Value ? Convert.ToInt32(reader["Codigo"]) : 0
                };
            }

            return new UsuarioEliminarDto { Mensaje = "Error inesperado", Codigo = 0 };
        }

        public async Task<UsuarioActualizarDto> ActualizarAsync(
    int idUsuario,
    int tipoDocumento,
    string numDocumento,
    string apePaterno,
    string apeMaterno,
    string nombres,
    string correoElectronico,
    string contrasenia,
    int codRol,
    bool activo)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_Usuarios_Actualizar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@iCodUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@iTipoDocumento", tipoDocumento);
            cmd.Parameters.AddWithValue("@vNumDocumento", numDocumento);
            cmd.Parameters.AddWithValue("@vApePaterno", apePaterno);
            cmd.Parameters.AddWithValue("@vApeMaterno", apeMaterno);
            cmd.Parameters.AddWithValue("@vNombres", nombres);
            cmd.Parameters.AddWithValue("@vCorreoElectronico", correoElectronico);
            string? hashedPassword = null;
            if (!string.IsNullOrWhiteSpace(contrasenia))
            {
                hashedPassword = _passwordHasher.HashPassword(null, contrasenia);
            }

            cmd.Parameters.AddWithValue("@vContrasenia", hashedPassword);

            cmd.Parameters.AddWithValue("@iCodRol", codRol);
            cmd.Parameters.AddWithValue("@bActivo", 1);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new UsuarioActualizarDto
                {
                    Mensaje = reader["Mensaje"]?.ToString() ?? string.Empty,
                    Codigo = reader["Codigo"] != DBNull.Value ? Convert.ToInt32(reader["Codigo"]) : 0
                };
            }

            return new UsuarioActualizarDto { Mensaje = "Error inesperado", Codigo = 0 };
        }


    }




}
 
