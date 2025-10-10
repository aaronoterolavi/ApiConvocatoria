using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class BonificacionesAdicionalesRepository : IBonificacionesAdicionalesRepository
    {
        private readonly string _connectionString;

        public BonificacionesAdicionalesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> InsertarAsync(BonificacionesAdicionalesDTO dto)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_InsertarBonificacionesAdicionales", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
            cmd.Parameters.AddWithValue("@bLicenciaFFAA", dto.bLicenciaFFAA);
            cmd.Parameters.AddWithValue("@vNroCarnetFFAA", dto.vNroCarnetFFAA ?? "");
            cmd.Parameters.AddWithValue("@bDiscapacidad", dto.bDiscapacidad);
            cmd.Parameters.AddWithValue("@vNroCarnetDiscapacidad", dto.vNroCarnetDiscapacidad ?? "");
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return mensajeParam.Value?.ToString() ?? "Sin mensaje";
        }

        public async Task<string> ActualizarAsync(BonificacionesAdicionalesDTO dto)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ActualizarBonificacionesAdicionales", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodBonificaciones", dto.iCodBonificaciones);
            cmd.Parameters.AddWithValue("@bLicenciaFFAA", dto.bLicenciaFFAA);
            cmd.Parameters.AddWithValue("@vNroCarnetFFAA", dto.vNroCarnetFFAA ?? "");
            cmd.Parameters.AddWithValue("@bDiscapacidad", dto.bDiscapacidad);
            cmd.Parameters.AddWithValue("@vNroCarnetDiscapacidad", dto.vNroCarnetDiscapacidad ?? "");
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return mensajeParam.Value?.ToString() ?? "Sin mensaje";
        }

        public async Task<string> EliminarAsync(int iCodBonificaciones)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_EliminarBonificacionesAdicionales", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodBonificaciones", iCodBonificaciones);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return mensajeParam.Value?.ToString() ?? "Sin mensaje";
        }

        public async Task<(List<BonificacionesAdicionalesDTO> lista, string mensaje)> ListarAsync(int? iCodUsuario)
        {
            var lista = new List<BonificacionesAdicionalesDTO>();
            string mensaje = string.Empty;

            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ListarBonificacionesAdicionales", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodUsuario", (object?)iCodUsuario ?? DBNull.Value);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                lista.Add(new BonificacionesAdicionalesDTO
                {
                    iCodBonificaciones = dr.GetInt32(dr.GetOrdinal("iCodBonificaciones")),
                    iCodUsuario = dr.GetInt32(dr.GetOrdinal("iCodUsuario")),
                    bLicenciaFFAA = dr.GetBoolean(dr.GetOrdinal("bLicenciaFFAA")),
                    vNroCarnetFFAA = dr["vNroCarnetFFAA"].ToString() ?? "",
                    bDiscapacidad = dr.GetBoolean(dr.GetOrdinal("bDiscapacidad")),
                    vNroCarnetDiscapacidad = dr["vNroCarnetDiscapacidad"].ToString() ?? "",
                    iCodUsuarioRegistra = dr.GetInt32(dr.GetOrdinal("iCodUsuarioRegistra")),
                    dtFechaRegistro = dr.GetDateTime(dr.GetOrdinal("dtFechaRegistro")),
                    bActivo = dr.GetBoolean(dr.GetOrdinal("bActivo"))
                });
            }

            mensaje = mensajeParam.Value?.ToString() ?? "Sin mensaje";
            return (lista, mensaje);
        }
    }
}
