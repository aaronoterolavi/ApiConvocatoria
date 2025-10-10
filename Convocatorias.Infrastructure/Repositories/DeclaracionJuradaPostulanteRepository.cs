using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class DeclaracionJuradaPostulanteRepository : IDeclaracionJuradaPostulanteRepository
    {
        private readonly string _connectionString;

        public DeclaracionJuradaPostulanteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> InsertarAsync(DeclaracionJuradaPostulanteDTO dto)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_InsertarDeclaracionJuradaPostulante", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
            cmd.Parameters.AddWithValue("@bSinAntecedentesPenales", dto.bSinAntecedentesPenales);
            cmd.Parameters.AddWithValue("@bSinProcesosJudiciales", dto.bSinProcesosJudiciales);
            cmd.Parameters.AddWithValue("@bSinSancionesAdministrativas", dto.bSinSancionesAdministrativas);
            cmd.Parameters.AddWithValue("@bSinVinculoLaboralEstado", dto.bSinVinculoLaboralEstado);
            cmd.Parameters.AddWithValue("@bAceptaBasesConcurso", dto.bAceptaBasesConcurso);
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

        public async Task<string> ActualizarAsync(DeclaracionJuradaPostulanteDTO dto)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ActualizarDeclaracionJuradaPostulante", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodDeclaracionJuradaPostulante", dto.iCodDeclaracionJuradaPostulante);
            cmd.Parameters.AddWithValue("@bSinAntecedentesPenales", dto.bSinAntecedentesPenales);
            cmd.Parameters.AddWithValue("@bSinProcesosJudiciales", dto.bSinProcesosJudiciales);
            cmd.Parameters.AddWithValue("@bSinSancionesAdministrativas", dto.bSinSancionesAdministrativas);
            cmd.Parameters.AddWithValue("@bSinVinculoLaboralEstado", dto.bSinVinculoLaboralEstado);
            cmd.Parameters.AddWithValue("@bAceptaBasesConcurso", dto.bAceptaBasesConcurso);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return mensajeParam.Value?.ToString() ?? "Sin mensaje";
        }

        public async Task<string> EliminarAsync(int iCodDeclaracionJuradaPostulante)
        {
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_EliminarDeclaracionJuradaPostulante", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodDeclaracionJuradaPostulante", iCodDeclaracionJuradaPostulante);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return mensajeParam.Value?.ToString() ?? "Sin mensaje";
        }

        public async Task<List<DeclaracionJuradaPostulanteDTO>> ListarAsync()
        {
            var lista = new List<DeclaracionJuradaPostulanteDTO>();

            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ListarDeclaracionJuradaPostulante", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            await cn.OpenAsync();
            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                lista.Add(new DeclaracionJuradaPostulanteDTO
                {
                    iCodDeclaracionJuradaPostulante = dr.GetInt32(dr.GetOrdinal("iCodDeclaracionJuradaPostulante")),
                    iCodUsuario = dr.GetInt32(dr.GetOrdinal("iCodUsuario")),
                    bSinAntecedentesPenales = dr.GetBoolean(dr.GetOrdinal("bSinAntecedentesPenales")),
                    bSinProcesosJudiciales = dr.GetBoolean(dr.GetOrdinal("bSinProcesosJudiciales")),
                    bSinSancionesAdministrativas = dr.GetBoolean(dr.GetOrdinal("bSinSancionesAdministrativas")),
                    bSinVinculoLaboralEstado = dr.GetBoolean(dr.GetOrdinal("bSinVinculoLaboralEstado")),
                    bAceptaBasesConcurso = dr.GetBoolean(dr.GetOrdinal("bAceptaBasesConcurso")),
                    iCodUsuarioRegistra = dr.GetInt32(dr.GetOrdinal("iCodUsuarioRegistra")),
                    dtFechaRegistro = dr.GetDateTime(dr.GetOrdinal("dtFechaRegistro")),
                    bActivo = dr.GetBoolean(dr.GetOrdinal("bActivo"))
                });
            }

            return lista;
        }
    }
}
