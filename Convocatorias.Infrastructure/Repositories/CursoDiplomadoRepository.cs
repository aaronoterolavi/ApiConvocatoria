using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class CursoDiplomadoRepository : ICursoDiplomadoRepository
    {
        private readonly string _connectionString;

        public CursoDiplomadoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> InsertAsync(CursoDiplomadoDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_CursoDiplomado_Insert", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            cmd.Parameters.AddWithValue("@vCurso", dto.vCurso ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vNombreInstitucion", dto.vNombreInstitucion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@iHoras", dto.iHoras);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<string> UpdateAsync(CursoDiplomadoDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_CursoDiplomado_Update", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodCursoDiplomado", dto.iCodCursoDiplomado);
            cmd.Parameters.AddWithValue("@vCurso", dto.vCurso ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@vNombreInstitucion", dto.vNombreInstitucion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@iHoras", dto.iHoras);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return "Registro actualizado correctamente.";
        }

        public async Task<string> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_CursoDiplomado_Delete", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iCodCursoDiplomado", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return "Registro eliminado correctamente.";
        }

        public async Task<CursoDiplomadoDto?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_CursoDiplomado_GetById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iCodCursoDiplomado", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new CursoDiplomadoDto
                {
                    iCodCursoDiplomado = reader.GetInt32(reader.GetOrdinal("iCodCursoDiplomado")),
                    iCodPostulante = reader.GetInt32(reader.GetOrdinal("iCodPostulante")),
                    vCurso = reader["vCurso"].ToString(),
                    vNombreInstitucion = reader["vNombreInstitucion"].ToString(),
                    iHoras = reader.GetInt32(reader.GetOrdinal("iHoras")),
                    dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                    iCodUsuarioRegistra = reader.GetInt32(reader.GetOrdinal("iCodUsuarioRegistra")),
                    bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                };
            }

            return null;
        }

        public async Task<List<CursoDiplomadoDto>> GetByPostulanteAsync(int iCodPostulante)
        {
            var lista = new List<CursoDiplomadoDto>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_CursoDiplomado_GetByPostulante", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iCodPostulante", iCodPostulante);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new CursoDiplomadoDto
                {
                    iCodCursoDiplomado = reader.GetInt32(reader.GetOrdinal("iCodCursoDiplomado")),
                    iCodPostulante = reader.GetInt32(reader.GetOrdinal("iCodPostulante")),
                    vCurso = reader["vCurso"].ToString(),
                    vNombreInstitucion = reader["vNombreInstitucion"].ToString(),
                    iHoras = reader.GetInt32(reader.GetOrdinal("iHoras")),
                    dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                    iCodUsuarioRegistra = reader.GetInt32(reader.GetOrdinal("iCodUsuarioRegistra")),
                    bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                });
            }

            return lista;
        }
    }
}
