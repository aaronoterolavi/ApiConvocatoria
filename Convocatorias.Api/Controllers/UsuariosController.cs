using System.Security.Claims;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Convocatorias.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _repo;
        private readonly IEmailService _email;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<object> _passwordHasher = new();

        public UsuariosController(IUsuarioRepository repo, IEmailService email, IConfiguration config)
        {
            _repo = repo;
            _email = email;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] UsuarioInsertarDto usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _repo.InsertarAsync(usuario);

            // Validación de respuesta desde el SP
            if (result.Codigo == 1)
            {
                // Registro exitoso
                return CreatedAtAction(nameof(Insertar), new { id = result.NuevoId }, result);
            }
            else if (result.Mensaje.Contains("duplicado", StringComparison.OrdinalIgnoreCase)
                  || result.Mensaje.Contains("existe", StringComparison.OrdinalIgnoreCase))
            {
                // Conflicto (ejemplo: documento ya registrado)
                return Conflict(result);
            }
            else
            {
                // Error controlado (no se insertó por alguna regla de negocio)
                return UnprocessableEntity(result); // 422
            }
        }

        // Login (público) — devuelve token dentro de LoginResponseDto
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _repo.LoginAsync(request);
            if (response == null)
                return Unauthorized(new { Mensaje = "Credenciales inválidas" });

            return Ok(response);
        }

        // Listar usuarios — protegido, solo rol = 3 puede acceder
       // [Authorize]
        [HttpGet("listar")]
        public async Task<IActionResult> Listar(
    int pageNumber = 1,
    int pageSize = 10,
    int? codRol = null,
    string? correo = null,
    string? nombreCompleto = null)
        {
            // Intentamos leer el claim "CodRol"
            var rolClaim = User.FindFirst("CodRol")?.Value
                          ?? User.FindFirst(ClaimTypes.Role)?.Value
                          ?? User.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(rolClaim) || !int.TryParse(rolClaim, out var rolUsuario))
                return Forbid();

            // Solo usuarios con rol 1 o 2 pueden listar
            if (rolUsuario != 1 )
                return Forbid();

            var result = await _repo.ListarAsync(pageNumber, pageSize, codRol, correo, nombreCompleto);
            return Ok(result);
        }

        // 1) Solicitar restablecimiento
        [HttpPost("olvide-contrasena")]
        [AllowAnonymous]
        public async Task<IActionResult> OlvideContrasena([FromBody] ForgotPasswordRequestDto dto)
        {
            // Siempre responder OK por seguridad (no filtrar si el correo existe)
            var userId = await _repo.GetUserIdByEmailAsync(dto.CorreoElectronico);
            if (userId == null)
                return Ok(new ForgotPasswordResponseDto { Exito = true, Mensaje = "Si el correo existe, recibirá instrucciones." });

            // Generar token
            var token = Guid.NewGuid().ToString("N");
            var expiryMinutes = int.Parse(_config["PasswordReset:TokenExpiryMinutes"] ?? "60");
            var expiracion = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var saved = await _repo.SavePasswordResetTokenAsync(userId.Value, token, expiracion);
            if (!saved)
                return Ok(new ForgotPasswordResponseDto { Exito = true, Mensaje = "Si el correo existe, recibirá instrucciones." });

            // Construir link hacia frontend
            var baseUrl = _config["Frontend:ResetUrlBase"]?.TrimEnd('/');
            var resetUrl = $"{baseUrl}?token={token}";

            // Obtener nombre opcional (si quieres mostrarlo), podrías tener un método para traer el nombre
            var displayName = dto.CorreoElectronico;

            // Enviar correo (no bloqueante idealmente)
            await _email.SendResetPasswordEmailAsync(dto.CorreoElectronico, displayName, resetUrl);

            return Ok(new ForgotPasswordResponseDto { Exito = true, Mensaje = "Si el correo existe, recibirá instrucciones." });
        }

        // 2) Validar token
        [HttpGet("validar-token")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidarToken([FromQuery] string token)
        {
            var userId = await _repo.GetUserIdByResetTokenAsync(token);
            if (userId == null)
                return BadRequest(new ValidateTokenResponseDto { Valido = false, Mensaje = "Token inválido o expirado." });

            return Ok(new ValidateTokenResponseDto { Valido = true, Mensaje = "Token válido." });
        }

        // 3) Reestablecer contraseña
        [HttpPost("reestablecer-contrasena")]
        [AllowAnonymous]
        public async Task<IActionResult> Reestablecer([FromBody] ResetPasswordRequestDto dto)
        {
            // Validar token y obtener usuario
            var userId = await _repo.GetUserIdByResetTokenAsync(dto.Token);
            if (userId == null)
                return BadRequest(new ResetPasswordResponseDto { Exito = false, Mensaje = "Token inválido o expirado." });

            // Hashear nueva contraseña
            var hashed = _passwordHasher.HashPassword(null, dto.NuevaContrasenia);

            var changed = await _repo.ResetPasswordByTokenAsync(dto.Token, hashed);
            if (!changed)
                return BadRequest(new ResetPasswordResponseDto { Exito = false, Mensaje = "No se pudo actualizar la contraseña." });

            return Ok(new ResetPasswordResponseDto { Exito = true, Mensaje = "Contraseña restablecida correctamente." });
        }


        [Authorize]
        [HttpDelete("eliminar/{idUsuario}")]
        public async Task<IActionResult> Eliminar(int idUsuario)
        {
            var rolClaim = User.FindFirst("CodRol")?.Value
                          ?? User.FindFirst(ClaimTypes.Role)?.Value
                          ?? User.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(rolClaim) || !int.TryParse(rolClaim, out var rolUsuario))
                return Forbid();

            if (rolUsuario != 1 )
                return Forbid();

            var result = await _repo.EliminarAsync(idUsuario);

            if (result.Codigo == 1)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("actualizar/{idUsuario}")]
        public async Task<IActionResult> Actualizar(
    int idUsuario,
    [FromBody] UsuarioActualizarRequest request)
        {
            var rolClaim = User.FindFirst("CodRol")?.Value
                          ?? User.FindFirst(ClaimTypes.Role)?.Value
                          ?? User.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(rolClaim) || !int.TryParse(rolClaim, out var rolUsuario))
                return Forbid();

            if (rolUsuario != 1 && rolUsuario != 2)
                return Forbid();

            var result = await _repo.ActualizarAsync(
                idUsuario,
                request.TipoDocumento,
                request.NumDocumento,
                request.ApePaterno,
                request.ApeMaterno,
                request.Nombres,
                request.CorreoElectronico,
                request.Contrasenia,
                request.CodRol
               // request.Activo
            );

            if (result.Codigo == 1)
                return Ok(result);

            return BadRequest(result);
        }

    }



}
