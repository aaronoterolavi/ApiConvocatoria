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
    public class FormatoArchivoPostulacionRepository : IFormatoArchivoPostulacionRepository
    {
        private readonly string _connectionString;

        public FormatoArchivoPostulacionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<FormatoArchivoPostulacionResponseDto> InsertarAsync(FormatoArchivoPostulacionInsertarDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_InsertarFormatoArchivoPostulacion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vDescripcion", dto.vDescripcion);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return new FormatoArchivoPostulacionResponseDto
            {
                Mensaje = mensajeParam.Value?.ToString()
            };
        }

        public async Task<FormatoArchivoPostulacionResponseDto> ActualizarAsync(FormatoArchivoPostulacionActualizarDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ActualizarFormatoArchivoPostulacion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodFormato", dto.iCodFormato);
            cmd.Parameters.AddWithValue("@vDescripcion", dto.vDescripcion);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return new FormatoArchivoPostulacionResponseDto
            {
                Mensaje = mensajeParam.Value?.ToString()
            };
        }

        public async Task<FormatoArchivoPostulacionResponseDto> EliminarAsync(int iCodFormato)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_EliminarFormatoArchivoPostulacion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodFormato", iCodFormato);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return new FormatoArchivoPostulacionResponseDto
            {
                Mensaje = mensajeParam.Value?.ToString()
            };
        }

        public async Task<List<FormatoArchivoPostulacionDto>> ListarAsync()
        {
            var lista = new List<FormatoArchivoPostulacionDto>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ListarFormatosArchivoPostulacion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new FormatoArchivoPostulacionDto
                {
                    iCodFormato = reader["iCodFormato"] != DBNull.Value ? Convert.ToInt32(reader["iCodFormato"]) : 0,
                    vDescripcion = reader["vDescripcion"].ToString() ?? string.Empty,
                    bActivo = reader["bActivo"] != DBNull.Value && Convert.ToBoolean(reader["bActivo"])
                });
            }

            return lista;
        }
    }
}
