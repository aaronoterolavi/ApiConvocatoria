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
    public class FormacionAcademicaRepository : IFormacionAcademicaRepository
    {
        private readonly string _connectionString;

        public FormacionAcademicaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<int> InsertarAsync(FormacionAcademicaDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_InsertarFormacionAcademica", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            command.Parameters.AddWithValue("@iCodNivelAcademico", dto.iCodNivelAcademico);
            command.Parameters.AddWithValue("@vInstitucion", dto.vInstitucion);
            command.Parameters.AddWithValue("@vProfesion", dto.vProfesion);
            command.Parameters.AddWithValue("@dFechaEgreso", dto.dFechaEgreso);
            command.Parameters.AddWithValue("@bActivo", dto.bActivo);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<IEnumerable<FormacionAcademicaDto>> ListarAsync()
        {
            var lista = new List<FormacionAcademicaDto>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ListarFormacionAcademica", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new FormacionAcademicaDto
                {
                    iCodFormacionAcademica = reader.GetInt32(0),
                    iCodPostulante = reader.GetInt32(1),
                    iCodNivelAcademico = reader.GetInt32(2),
                    vInstitucion = reader.GetString(3),
                    vProfesion = reader.GetString(4),
                    dFechaEgreso = reader.GetDateTime(5),
                    dtFechaRegistro = reader.GetDateTime(6),
                    bActivo = reader.GetBoolean(7)
                });
            }

            return lista;
        }

        public async Task<FormacionAcademicaDto?> ObtenerPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ObtenerFormacionAcademicaPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iCodFormacionAcademica", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new FormacionAcademicaDto
                {
                    iCodFormacionAcademica = reader.GetInt32(0),
                    iCodPostulante = reader.GetInt32(1),
                    iCodNivelAcademico = reader.GetInt32(2),
                    vInstitucion = reader.GetString(3),
                    vProfesion = reader.GetString(4),
                    dFechaEgreso = reader.GetDateTime(5),
                    dtFechaRegistro = reader.GetDateTime(6),
                    bActivo = reader.GetBoolean(7)
                };
            }
            return null;
        }

        public async Task<string> ActualizarAsync(FormacionAcademicaDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ActualizarFormacionAcademica", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodFormacionAcademica", dto.iCodFormacionAcademica);
            command.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            command.Parameters.AddWithValue("@iCodNivelAcademico", dto.iCodNivelAcademico);
            command.Parameters.AddWithValue("@vInstitucion", dto.vInstitucion);
            command.Parameters.AddWithValue("@vProfesion", dto.vProfesion);
            command.Parameters.AddWithValue("@dFechaEgreso", dto.dFechaEgreso);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result?.ToString() ?? "Registro actualizado correctamente.";
        }

        public async Task<string> EliminarAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_EliminarFormacionAcademica", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iCodFormacionAcademica", id);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result?.ToString() ?? "Registro eliminado.";
        }
    }
}
