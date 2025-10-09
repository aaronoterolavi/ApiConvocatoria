using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class ColegiaturaRepository : IColegiaturaRepository
    {
        private readonly string _connectionString;

        public ColegiaturaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<int> InsertarAsync(ColegiaturaDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_InsertarColegiatura", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            command.Parameters.AddWithValue("@iCodColegioProfesional", dto.iCodColegioProfesional);
            command.Parameters.AddWithValue("@vNroColegiatura", dto.vNroColegiatura);
            command.Parameters.AddWithValue("@bHabilitado", dto.bHabilitado);
            command.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<IEnumerable<ColegiaturaDto>> ListarAsync()
        {
            var lista = new List<ColegiaturaDto>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ListarColegiaturas", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new ColegiaturaDto
                {
                    iCodColegiatura = reader.GetInt32(0),
                    iCodPostulante = reader.GetInt32(1),
                    iCodColegioProfesional = reader.GetInt32(2),
                    vNroColegiatura = reader.GetString(3),
                    bHabilitado = reader.GetBoolean(4),
                    iCodUsuarioRegistra = reader.GetInt32(5),
                    dtFechaRegistro = reader.GetDateTime(6),
                    bActivo = reader.GetBoolean(7)
                });
            }

            return lista;
        }

        public async Task<ColegiaturaDto?> ObtenerPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ObtenerColegiaturaPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iCodColegiatura", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ColegiaturaDto
                {
                    iCodColegiatura = reader.GetInt32(0),
                    iCodPostulante = reader.GetInt32(1),
                    iCodColegioProfesional = reader.GetInt32(2),
                    vNroColegiatura = reader.GetString(3),
                    bHabilitado = reader.GetBoolean(4),
                    iCodUsuarioRegistra = reader.GetInt32(5),
                    dtFechaRegistro = reader.GetDateTime(6),
                    bActivo = reader.GetBoolean(7)
                };
            }

            return null;
        }

        public async Task<string> ActualizarAsync(ColegiaturaDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ActualizarColegiatura", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodColegiatura", dto.iCodColegiatura);
            command.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            command.Parameters.AddWithValue("@iCodColegioProfesional", dto.iCodColegioProfesional);
            command.Parameters.AddWithValue("@vNroColegiatura", dto.vNroColegiatura);
            command.Parameters.AddWithValue("@bHabilitado", dto.bHabilitado);
            command.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result?.ToString() ?? "Registro actualizado correctamente.";
        }

        public async Task<string> EliminarAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_EliminarColegiatura", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iCodColegiatura", id);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result?.ToString() ?? "Registro eliminado lógicamente.";
        }
    }
}
