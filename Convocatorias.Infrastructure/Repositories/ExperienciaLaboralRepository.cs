using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class ExperienciaLaboralRepository : IExperienciaLaboralRepository
    {
        private readonly string _connectionString;

        public ExperienciaLaboralRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> InsertarAsync(ExperienciaLaboralDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarExperienciaLaboral", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            cmd.Parameters.AddWithValue("@vEntidad", dto.vEntidad ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vUnidadOrganica", dto.vUnidadOrganica ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vCargo", dto.vCargo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@cSector", dto.cSector ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@dFechaInicio", dto.dFechaInicio);
            cmd.Parameters.AddWithValue("@dFechaFin", dto.dFechaFin ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vFunciones", dto.vFunciones ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<List<ExperienciaLaboralDto>> ListarAsync()
        {
            var lista = new List<ExperienciaLaboralDto>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ListarExperienciaLaboral", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new ExperienciaLaboralDto
                {
                    iCodExperienciaLaboral = reader.GetInt32(reader.GetOrdinal("iCodExperienciaLaboral")),
                    iCodPostulante = reader.GetInt32(reader.GetOrdinal("iCodPostulante")),
                    vEntidad = reader["vEntidad"].ToString(),
                    vUnidadOrganica = reader["vUnidadOrganica"].ToString(),
                    vCargo = reader["vCargo"].ToString(),
                    cSector = reader["cSector"].ToString(),
                    dFechaInicio = reader.GetDateTime(reader.GetOrdinal("dFechaInicio")),
                    dFechaFin = reader["dFechaFin"] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("dFechaFin")),
                    vFunciones = reader["vFunciones"].ToString(),
                    iCodUsuarioRegistra = reader.GetInt32(reader.GetOrdinal("iCodUsuarioRegistra")),
                    dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                    bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                });
            }

            return lista;
        }

        public async Task<ExperienciaLaboralDto?> ObtenerPorIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ObtenerExperienciaLaboralPorId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iCodExperienciaLaboral", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ExperienciaLaboralDto
                {
                    iCodExperienciaLaboral = reader.GetInt32(reader.GetOrdinal("iCodExperienciaLaboral")),
                    iCodPostulante = reader.GetInt32(reader.GetOrdinal("iCodPostulante")),
                    vEntidad = reader["vEntidad"].ToString(),
                    vUnidadOrganica = reader["vUnidadOrganica"].ToString(),
                    vCargo = reader["vCargo"].ToString(),
                    cSector = reader["cSector"].ToString(),
                    dFechaInicio = reader.GetDateTime(reader.GetOrdinal("dFechaInicio")),
                    dFechaFin = reader["dFechaFin"] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("dFechaFin")),
                    vFunciones = reader["vFunciones"].ToString(),
                    iCodUsuarioRegistra = reader.GetInt32(reader.GetOrdinal("iCodUsuarioRegistra")),
                    dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                    bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                };
            }

            return null;
        }

        public async Task<string> ActualizarAsync(ExperienciaLaboralDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarExperienciaLaboral", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodExperienciaLaboral", dto.iCodExperienciaLaboral);
            cmd.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            cmd.Parameters.AddWithValue("@vEntidad", dto.vEntidad ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vUnidadOrganica", dto.vUnidadOrganica ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vCargo", dto.vCargo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@cSector", dto.cSector ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@dFechaInicio", dto.dFechaInicio);
            cmd.Parameters.AddWithValue("@dFechaFin", dto.dFechaFin ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vFunciones", dto.vFunciones ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return result?.ToString() ?? "Actualizado correctamente";
        }

        public async Task<string> EliminarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarExperienciaLaboral", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iCodExperienciaLaboral", id);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return result?.ToString() ?? "Eliminado correctamente";
        }
    }
}
