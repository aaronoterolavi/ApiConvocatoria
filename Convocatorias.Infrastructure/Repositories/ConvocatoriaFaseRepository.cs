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
    public class ConvocatoriaFaseRepository : IConvocatoriaFaseRepository
    {
        private readonly string _connectionString;


        public ConvocatoriaFaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ConvocatoriaFaseResponseDto> InsertarAsync(ConvocatoriaFaseInsertarDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_ConvocatoriaFase_Insertar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodConvocatoria", dto.CodConvocatoria);
            cmd.Parameters.AddWithValue("@iCodEstado", dto.CodEstado);
            cmd.Parameters.AddWithValue("@dtFechaInicio", dto.FechaInicio);
            cmd.Parameters.AddWithValue("@dtFechaFin", dto.FechaFin);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.CodUsuarioRegistra);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ConvocatoriaFaseResponseDto
                {
                    NuevoId = reader["NuevoId"] != DBNull.Value ? Convert.ToInt32(reader["NuevoId"]) : 0,
                    Mensaje = reader["Mensaje"].ToString() ?? string.Empty,
                    Codigo = reader["Codigo"] != DBNull.Value ? Convert.ToInt32(reader["Codigo"]) : 0
                };
            }

            return new ConvocatoriaFaseResponseDto
            {
                NuevoId = 0,
                Mensaje = "No se devolvió resultado",
                Codigo = 0
            };
        }

        public async Task<IEnumerable<ConvocatoriaFaseDto>> ListarPorConvocatoriaAsync(int iCodConvocatoria)
        {
            var fases = new List<ConvocatoriaFaseDto>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ListarFasePorConvocatoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                fases.Add(new ConvocatoriaFaseDto
                {
                    iCodFase = reader["iCodFase"] != DBNull.Value ? Convert.ToInt32(reader["iCodFase"]) : 0,
                    iCodConvocatoria = reader["iCodConvocatoria"] != DBNull.Value ? Convert.ToInt32(reader["iCodConvocatoria"]) : 0,
                    iCodEstado = reader["iCodEstado"] != DBNull.Value ? Convert.ToInt32(reader["iCodEstado"]) : 0,
                    vDescripcionEstado = reader["vDescripcionEstado"].ToString() ?? string.Empty,
                    dtFechaInicio = reader["dtFechaInicio"] != DBNull.Value ? Convert.ToDateTime(reader["dtFechaInicio"]) : DateTime.MinValue,
                    dtFechaFin = reader["dtFechaFin"] != DBNull.Value ? Convert.ToDateTime(reader["dtFechaFin"]) : DateTime.MinValue,
                    bActivo = reader["bActivo"] != DBNull.Value && Convert.ToBoolean(reader["bActivo"])
                });
            }

            return fases;
        }

    }
}
