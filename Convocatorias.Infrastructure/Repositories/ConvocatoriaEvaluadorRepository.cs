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
    public class ConvocatoriaEvaluadorRepository : IConvocatoriaEvaluadorRepository
    {
        private readonly string _connectionString;

        public ConvocatoriaEvaluadorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<string> AsignarEvaluadorAConvocatoriaAsync(int iCodConvocatoria, int iCodUsuarioEvaluador, int iCodUsuarioRegistra)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_AsignarEvaluadorAConvocatoria", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria);
            cmd.Parameters.AddWithValue("@iCodUsuarioEvaluador", iCodUsuarioEvaluador);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", iCodUsuarioRegistra);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return mensajeParam.Value?.ToString() ?? "Operación completada.";
        }

        public async Task<string> DesactivarAsignacionEvaluadorAsync(int iCodConvocatoria, int iCodUsuarioEvaluador, int iCodUsuarioAccion)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_DesactivarAsignacionEvaluador", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria);
            cmd.Parameters.AddWithValue("@iCodUsuarioEvaluador", iCodUsuarioEvaluador);
            cmd.Parameters.AddWithValue("@iCodUsuarioAccion", iCodUsuarioAccion);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return mensajeParam.Value?.ToString() ?? "Operación completada.";
        }

        public async Task<List<ConvocatoriaEvaluadorDto>> ListarEvaluadoresPorConvocatoriaAsync(int iCodConvocatoria)
        {
            var lista = new List<ConvocatoriaEvaluadorDto>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_ListarEvaluadoresPorConvocatoria", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new ConvocatoriaEvaluadorDto
                {
                    iCodConvocatoriaEvaluador = reader.GetInt32(reader.GetOrdinal("iCodConvocatoriaEvaluador")),
                    iCodConvocatoria = reader.GetInt32(reader.GetOrdinal("iCodConvocatoria")),
                    iCodUsuarioEvaluador = reader.GetInt32(reader.GetOrdinal("iCodUsuarioEvaluador")),
                    vNombres = reader["vNombres"]?.ToString(),
                    vApePaterno = reader["vApePaterno"]?.ToString(),
                    vApeMaterno = reader["vApeMaterno"]?.ToString(),
                    vCorreoElectronico = reader["vCorreoElectronico"]?.ToString(),
                    iCodRol = reader.GetInt32(reader.GetOrdinal("iCodRol")),
                    bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo")),
                    dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro"))
                });
            }

            return lista;
        }

        public async Task<List<ConvocatoriaEvaluadorDto>> ListarConvocatoriasPorEvaluadorAsync(int iCodUsuarioEvaluador)
        {
            var lista = new List<ConvocatoriaEvaluadorDto>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_ListarConvocatoriasPorEvaluador", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@iCodUsuarioEvaluador", iCodUsuarioEvaluador);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new ConvocatoriaEvaluadorDto
                {
                    iCodConvocatoria = reader.GetInt32(reader.GetOrdinal("iCodConvocatoria")),
                    vTitulo = reader["vTitulo"]?.ToString(),
                    iCodTipoConvocatoria = reader["iCodTipoConvocatoria"] as int?,
                    dtFechaInicio = reader["dtFechaInicio"] as DateTime?,
                    dtFechaFin = reader["dtFechaFin"] as DateTime?,
                    vRequisitos = reader["vRequisitos"]?.ToString(),
                    iCodUnidadZonal = reader["iCodUnidadZonal"] as int?,
                    bActivoConvocatoria = reader["bActivoConvocatoria"] as bool?,
                    iCodConvocatoriaEvaluador = reader["iCodConvocatoriaEvaluador"] as int? ?? 0,
                    dtFechaAsignacion = reader["dtFechaAsignacion"] as DateTime?
                });
            }

            return lista;
        }
    }
}
