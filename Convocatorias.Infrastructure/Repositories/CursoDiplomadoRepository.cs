using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class CursoDiplomadoRepository : ICursoDiplomadoRepository
    {
        private readonly string _connectionString;

        public CursoDiplomadoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task InsertarAsync(CursoDiplomadoDTO entidad)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_CursoDiplomado_Insertar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodUsuario", entidad.iCodUsuario);
                cmd.Parameters.AddWithValue("@vCurso", entidad.vCurso);
                cmd.Parameters.AddWithValue("@vNombreInstitucion", entidad.vNombreInstitucion);
                cmd.Parameters.AddWithValue("@iHoras", entidad.iHoras);
                cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", entidad.iCodUsuarioRegistra);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<CursoDiplomadoDTO>> ListarAsync(int iCodUsuario)
        {
            var lista = new List<CursoDiplomadoDTO>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_CursoDiplomado_Listar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new CursoDiplomadoDTO
                        {
                            iCodCursoDiplomado = reader.GetInt32(reader.GetOrdinal("iCodCursoDiplomado")),
                            iCodUsuario = reader.GetInt32(reader.GetOrdinal("iCodUsuario")),
                            vCurso = reader.GetString(reader.GetOrdinal("vCurso")),
                            vNombreInstitucion = reader.GetString(reader.GetOrdinal("vNombreInstitucion")),
                            iHoras = reader.GetInt32(reader.GetOrdinal("iHoras")),
                            dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                            iCodUsuarioRegistra = reader.GetInt32(reader.GetOrdinal("iCodUsuarioRegistra")),
                            bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                        });
                    }
                }
            }

            return lista;
        }

        public async Task ActualizarAsync(CursoDiplomadoDTO entidad)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_CursoDiplomado_Actualizar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodCursoDiplomado", entidad.iCodCursoDiplomado);
                cmd.Parameters.AddWithValue("@vCurso", entidad.vCurso);
                cmd.Parameters.AddWithValue("@vNombreInstitucion", entidad.vNombreInstitucion);
                cmd.Parameters.AddWithValue("@iHoras", entidad.iHoras);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarAsync(int iCodCursoDiplomado)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_CursoDiplomado_Eliminar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodCursoDiplomado", iCodCursoDiplomado);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
