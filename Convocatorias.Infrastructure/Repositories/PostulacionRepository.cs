using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class PostulacionRepository : IPostulacionRepository
    {
        private readonly string _connectionString;

        public PostulacionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<string> InsertarAsync(int iCodUsuario, int iCodConvocatoria, int iCodUsuarioRegistra)
        {
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("[dbo].[PA_InsertarPostulacion]", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);
            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", iCodUsuarioRegistra);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            try
            {
                await cmd.ExecuteNonQueryAsync();
                return (mensajeParam.Value ?? string.Empty).ToString()!;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> ActualizarAsync(int iCodPostulacion, int iCodUsuario, int iCodConvocatoria, int iCodUsuarioRegistra)
        {
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("[dbo].[PA_ActualizarPostulacion]", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodPostulacion", iCodPostulacion);
            cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);
            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", iCodUsuarioRegistra);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            try
            {
                await cmd.ExecuteNonQueryAsync();
                return (mensajeParam.Value ?? string.Empty).ToString()!;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EliminarAsync(int iCodPostulacion)
        {
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("[dbo].[PA_EliminarPostulacion]", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodPostulacion", iCodPostulacion);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            try
            {
                await cmd.ExecuteNonQueryAsync();
                return (mensajeParam.Value ?? string.Empty).ToString()!;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<PostulacionDto>> ListarAsync(int? iCodUsuario, int? iCodConvocatoria, bool soloActivos)
        {
            var resultado = new List<PostulacionDto>();

            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("[dbo].[PA_ListarPostulaciones]", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodUsuario", (object?)iCodUsuario ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodConvocatoria", (object?)iCodConvocatoria ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SoloActivos", soloActivos ? 1 : 0);

            await cn.OpenAsync();
            await using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                var dto = new PostulacionDto
                {
                    iCodPostulacion = Get<int>(dr, "iCodPostulacion"),
                    iCodUsuario = Get<int>(dr, "iCodUsuario"),
                    iCodConvocatoria = Get<int>(dr, "iCodConvocatoria"),
                    dtFechaPostulacion = Get<DateTime?>(dr, "dtFechaPostulacion"),
                    iCodUsuarioRegistra = Get<int?>(dr, "iCodUsuarioRegistra"),
                    bActivo = Get<bool>(dr, "bActivo")
                };
                resultado.Add(dto);
            }

            return resultado;
        }

        public async Task<(List<PostulacionDto> Items, int TotalRegistros)> ListarPaginadoAsync(
            int? iCodPostulacion,
            int? iCodUsuario,
            int? iCodConvocatoria,
            int? iCodTipoConvocatoria,
            int? iCodUnidadZonal,
            string? vNumDocumento,
            string? vNombreCompleto,
            string? vTituloConvocatoria,
            string? vTipoConvocatoria,
            string? vUnidadZonal,
            DateTime? FechaPostulacionDesde,
            DateTime? FechaPostulacionHasta,
            bool soloActivos,
            int PageNumber,
            int PageSize)
        {
            var items = new List<PostulacionDto>();
            int total = 0;

            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("[dbo].[PA_ListarPostulacionesPaginado]", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodPostulacion", (object?)iCodPostulacion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodUsuario", (object?)iCodUsuario ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodConvocatoria", (object?)iCodConvocatoria ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodTipoConvocatoria", (object?)iCodTipoConvocatoria ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodUnidadZonal", (object?)iCodUnidadZonal ?? DBNull.Value);
          

            cmd.Parameters.AddWithValue("@vNumDocumento", (object?)vNumDocumento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vNombreCompleto", (object?)vNombreCompleto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vTituloConvocatoria", (object?)vTituloConvocatoria ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vTipoConvocatoria", (object?)vTipoConvocatoria ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vUnidadZonal", (object?)vUnidadZonal ?? DBNull.Value);
            

            cmd.Parameters.AddWithValue("@FechaPostulacionDesde", (object?)FechaPostulacionDesde ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaPostulacionHasta", (object?)FechaPostulacionHasta ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@SoloActivos", soloActivos ? 1 : 0);
            cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);

            await cn.OpenAsync();
            await using var dr = await cmd.ExecuteReaderAsync();

            // 1er resultset: filas
            while (await dr.ReadAsync())
            {
                var dto = new PostulacionDto
                {
                    iCodPostulacion = Get<int>(dr, "iCodPostulacion"),
                    iCodUsuario = Get<int>(dr, "iCodUsuario"),
                    vNumDocumento = Get<string?>(dr, "vNumDocumento"),
                    vNombreCompleto = Get<string?>(dr, "vNombreCompleto"),
                    vCorreoElectronico = Get<string?>(dr, "vCorreoElectronico"),
                    iCodConvocatoria = Get<int>(dr, "iCodConvocatoria"),
                    vTituloConvocatoria = Get<string?>(dr, "vTituloConvocatoria"),
                    iCodTipoConvocatoria = Get<int?>(dr, "iCodTipoConvocatoria"),
                    vTipoConvocatoria = Get<string?>(dr, "vTipoConvocatoria"),
                    iCodEstadoConvocatoria = Get<int?>(dr, "iCodEstadoConvocatoria"),
                    vEstadoConvocatoria = Get<string?>(dr, "vEstadoConvocatoria"),
                    iCodUnidadZonal = Get<int?>(dr, "iCodUnidadZonal"),
                    vUnidadZonal = Get<string?>(dr, "vUnidadZonal"),
                    dtFechaInicio = Get<DateTime?>(dr, "dtFechaInicio"),
                    dtFechaFin = Get<DateTime?>(dr, "dtFechaFin"),
                    vRequisitos = Get<string?>(dr, "vRequisitos"),
                    dtFechaPostulacion = Get<DateTime?>(dr, "dtFechaPostulacion"),
                    iCodUsuarioRegistra = Get<int?>(dr, "iCodUsuarioRegistra"),
                    bActivo = Get<bool>(dr, "bActivo")
                };
                items.Add(dto);
            }

            // 2do resultset: total
            if (await dr.NextResultAsync() && await dr.ReadAsync())
            {
                total = Get<int>(dr, "TotalRegistros");
            }

            return (items, total);
        }

        // ==== Helpers locales (sin clases adicionales) ====
        private static T Get<T>(IDataRecord r, string colName)
        {
            var ordinal = SafeOrdinal(r, colName);
            if (ordinal < 0 || r.IsDBNull(ordinal)) return default!;
            return (T)r.GetValue(ordinal);
        }

        private static int SafeOrdinal(IDataRecord r, string colName)
        {
            try
            {
                return r.GetOrdinal(colName);
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }
    }
}
