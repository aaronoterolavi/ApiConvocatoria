using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class FormacionAcademicaRepository : IFormacionAcademicaRepository
    {
        private readonly string _connectionString;

        public FormacionAcademicaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<FormacionAcademicaDTO>> ListarAsync()
        {
            var lista = new List<FormacionAcademicaDTO>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_ListarFormacionAcademica", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(Map(reader));
            }

            return lista;
        }

        public async Task<IEnumerable<FormacionAcademicaDTO>> ListarPorUsuarioAsync(int iCodUsuario)
        {
            var lista = new List<FormacionAcademicaDTO>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_ListarFormacionAcademicaPorUsuario", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(Map(reader));
            }

            return lista;
        }

        public async Task<int> InsertarAsync(FormacionAcademicaDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_InsertarFormacionAcademica", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
            command.Parameters.AddWithValue("@iCodNivelAcademico", dto.iCodNivelAcademico);
            command.Parameters.AddWithValue("@vInstitucion", dto.vInstitucion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vProfesion", dto.vProfesion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@dFechaEgreso", dto.dFechaEgreso);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task ActualizarAsync(FormacionAcademicaDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_ActualizarFormacionAcademica", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodFormacionAcademica", dto.iCodFormacionAcademica);
            command.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
            command.Parameters.AddWithValue("@iCodNivelAcademico", dto.iCodNivelAcademico);
            command.Parameters.AddWithValue("@vInstitucion", dto.vInstitucion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vProfesion", dto.vProfesion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@dFechaEgreso", dto.dFechaEgreso);
            command.Parameters.AddWithValue("@bActivo", dto.bActivo);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(int iCodFormacionAcademica)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_EliminarFormacionAcademica", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodFormacionAcademica", iCodFormacionAcademica);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        private FormacionAcademicaDTO Map(SqlDataReader reader)
        {
            return new FormacionAcademicaDTO
            {
                iCodFormacionAcademica = reader.GetInt32(reader.GetOrdinal("iCodFormacionAcademica")),
                iCodUsuario = reader.GetInt32(reader.GetOrdinal("iCodUsuario")),
                iCodNivelAcademico = reader.GetInt32(reader.GetOrdinal("iCodNivelAcademico")),
                vInstitucion = reader["vInstitucion"].ToString(),
                vProfesion = reader["vProfesion"].ToString(),
                dFechaEgreso = reader.GetDateTime(reader.GetOrdinal("dFechaEgreso")),
                dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
            };
        }
    }
}
