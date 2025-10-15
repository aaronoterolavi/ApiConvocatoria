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
    public class ConvocatoriaRepository : IConvocatoriaRepository
    {
        private readonly string _connectionString;

        public ConvocatoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ConvocatoriaInsertarResponseDto> InsertarAsync(ConvocatoriaInsertarDto dto)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("USP_Convocatorias_Insertar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@vTitulo", dto.Titulo);
                    command.Parameters.AddWithValue("@iCodTipoConvocatoria", dto.CodTipoConvocatoria);
                    command.Parameters.AddWithValue("@dtFechaInicio", dto.FechaInicio);
                    command.Parameters.AddWithValue("@dtFechaFin", dto.FechaFin);
                    command.Parameters.AddWithValue("@iCodUnidadZonal", dto.CodUnidadZonal);
                    command.Parameters.AddWithValue("@vRequisitos", (object?)dto.Requisitos ?? DBNull.Value);
                    command.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.CodUsuarioRegistra);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Helper local para comprobar existencia de columna
                            static bool HasColumn(IDataRecord r, string name)
                            {
                                for (int i = 0; i < r.FieldCount; i++)
                                {
                                    if (string.Equals(r.GetName(i), name, StringComparison.OrdinalIgnoreCase))
                                        return true;
                                }
                                return false;
                            }

                            var response = new ConvocatoriaInsertarResponseDto();

                            // NuevoId puede no existir si hay error; maneja ambos casos
                            if (HasColumn(reader, "NuevoId") && reader["NuevoId"] != DBNull.Value)
                                response.NuevoId = Convert.ToInt32(reader["NuevoId"]);

                            // Mensaje debería existir (en ambos casos)
                            if (HasColumn(reader, "Mensaje") && reader["Mensaje"] != DBNull.Value)
                                response.Mensaje = reader["Mensaje"].ToString() ?? string.Empty;

                            // Codigo (1 o 0) suele venir en la respuesta
                            if (HasColumn(reader, "Codigo") && reader["Codigo"] != DBNull.Value)
                                response.Codigo = Convert.ToInt32(reader["Codigo"]);
                            else
                                response.Codigo = 0;

                            return response;
                        }
                    }

                    return new ConvocatoriaInsertarResponseDto
                    {
                        Codigo = 0,
                        Mensaje = "No se obtuvo respuesta del procedimiento almacenado."
                    };
                }
            }
            catch (Exception ex)
            {
                // Puedes loggear ex.Message aquí con tu logger preferido
                return new ConvocatoriaInsertarResponseDto
                {
                    Codigo = 0,
                    Mensaje = "Error al ejecutar el procedimiento: " + ex.Message
                };
            }
        }

      

        public async Task<ConvocatoriaPaginadaDto> ListarConvocatoriasPaginadoAsync(
            int pageNumber, int pageSize, DateTime? fechaInicio, DateTime? fechaFin, int? codTipoConvocatoria,bool? bActivo, string? buscar)
        {
            var result = new ConvocatoriaPaginadaDto();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("USP_Convocatorias_ListarPaginado", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PageNumber", pageNumber);
            command.Parameters.AddWithValue("@PageSize", pageSize);
            command.Parameters.AddWithValue("@FechaInicio", (object?)fechaInicio ?? DBNull.Value);
            command.Parameters.AddWithValue("@FechaFin", (object?)fechaFin ?? DBNull.Value);
            command.Parameters.AddWithValue("@CodTipoConvocatoria", (object?)codTipoConvocatoria ?? DBNull.Value);
            command.Parameters.AddWithValue("@bActivo", (object?)bActivo ?? DBNull.Value);
            command.Parameters.AddWithValue("@Buscar", (object?)buscar ?? DBNull.Value);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Items.Add(new ConvocatoriaDto
                {
                    iCodConvocatoria = reader.GetInt32(0),
                    vTitulo = reader.GetString(1),
                    iCodTipoDocumento =reader.GetInt32(2),
                    vDescripcionConvocatoria = reader.GetString(3),
                    dtFechaInicio = reader.GetDateTime(4),
                    dtFechaFin = reader.GetDateTime(5),
                    vRequisitos = reader.GetString(6),
                    iCodUnidadZonal = reader.GetInt32(7),
                    vUnidadZonal = reader.GetString(8),
                    bActivo = reader.GetBoolean(11)
                });

                result.TotalRecords = reader.GetInt32(12); // Columna TotalRecords
            }

            return result;
        }

        public async Task<ResponseDto> ActualizarAsync(ConvocatoriaUpdateDto dto)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_Convocatorias_Actualizar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iCodConvocatoria", dto.iCodConvocatoria);
                command.Parameters.AddWithValue("@vTitulo", dto.vTitulo);
                command.Parameters.AddWithValue("@iCodTipoConvocatoria", dto.iCodTipoConvocatoria);
                command.Parameters.AddWithValue("@dtFechaInicio", dto.dtFechaInicio);
                command.Parameters.AddWithValue("@dtFechaFin", dto.dtFechaFin);
                command.Parameters.AddWithValue("@vRequisitos", dto.vRequisitos);
                command.Parameters.AddWithValue("@iCodUnidadZonal", dto.iCodUnidadZonal);
                command.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ResponseDto
                        {
                            Id = reader["IdActualizado"] != DBNull.Value ? (int)reader["IdActualizado"] : 0,
                            Mensaje = reader["Mensaje"].ToString(),
                            Codigo = reader["Codigo"] != DBNull.Value ? (int)reader["Codigo"] : 0
                        };
                    }
                }
            }
            return new ResponseDto { Id = 0, Mensaje = "No se pudo actualizar", Codigo = 0 };
        }

        public async Task<ResponseDto> EliminarAsync(int idConvocatoria)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_Convocatorias_Eliminar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iCodConvocatoria", idConvocatoria);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ResponseDto
                        {
                            Id = reader["IdEliminado"] != DBNull.Value ? (int)reader["IdEliminado"] : 0,
                            Mensaje = reader["Mensaje"].ToString(),
                            Codigo = reader["Codigo"] != DBNull.Value ? (int)reader["Codigo"] : 0
                        };
                    }
                }
            }
            return new ResponseDto { Id = 0, Mensaje = "No se pudo eliminar", Codigo = 0 };
        }

        public async Task<List<ConvocatoriaConFaseDto>> ListarConvocatoriasConFasePaginado(
            int? iCodTipoConvocatoria,
            int? iCodUnidadZonal,
            DateTime? FechaInicio,
            DateTime? FechaFin,
            string? FiltroGeneral,
            int PageNumber,
            int PageSize)
        {
            var lista = new List<ConvocatoriaConFaseDto>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_ListarConvocatoriasConFasePaginado", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodTipoConvocatoria", (object?)iCodTipoConvocatoria ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iCodUnidadZonal", (object?)iCodUnidadZonal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaInicio", (object?)FechaInicio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaFin", (object?)FechaFin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FiltroGeneral", (object?)FiltroGeneral ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ConvocatoriaConFaseDto
                        {
                            iCodEstadoConvocatoria = reader["iCodEstadoConvocatoria"].ToString() ?? "",
                            vEstadoConvocatoria = reader["vEstadoConvocatoria"].ToString() ?? "",
                            iCodConvocatoria = Convert.ToInt32(reader["iCodConvocatoria"]),
                            vTitulo = reader["vTitulo"].ToString() ?? "",
                            iCodTipoConvocatoria = Convert.ToInt32(reader["iCodTipoConvocatoria"]),
                            vTipoConvocatoria = reader["vTipoConvocatoria"].ToString() ?? "",
                            dtFechaInicio = Convert.ToDateTime(reader["dtFechaInicio"]),
                            dtFechaFin = Convert.ToDateTime(reader["dtFechaFin"]),
                            vRequisitos = reader["vRequisitos"].ToString() ?? "",
                            iCodUnidadZonal = Convert.ToInt32(reader["iCodUnidadZonal"]),
                            vUnidadZonal = reader["vUnidadZonal"].ToString() ?? "",
                            iCodUsuarioRegistra = Convert.ToInt32(reader["iCodUsuarioRegistra"]),
                            dtFechaRegistro = Convert.ToDateTime(reader["dtFechaRegistro"]),
                            bActivo = Convert.ToBoolean(reader["bActivo"])
                        });
                    }

                    // Segunda consulta: total de registros
                    if (await reader.NextResultAsync() && await reader.ReadAsync())
                    {
                        int total = Convert.ToInt32(reader["TotalRegistros"]);
                        foreach (var item in lista)
                            item.TotalRegistros = total;
                    }
                }
            }

            return lista;
        }
    }
}
