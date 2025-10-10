using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class IdiomaRepository : IIdiomaRepository
    {
        private readonly string _connectionString;

        public IdiomaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> InsertarAsync(IdiomaDTO entidad)
        {
            string mensaje = string.Empty;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_Idioma_Insertar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodUsuario", entidad.iCodUsuario);
                cmd.Parameters.AddWithValue("@vIdioma", entidad.vIdioma);
                cmd.Parameters.AddWithValue("@vInstitucion", entidad.vInstitucion);
                cmd.Parameters.AddWithValue("@vNivelAlcanzado", entidad.vNivelAlcanzado);
                cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", entidad.iCodUsuarioRegistra);

                SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pMensaje);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                mensaje = pMensaje.Value.ToString();
            }

            return mensaje;
        }

        public async Task<IEnumerable<IdiomaDTO>> ListarAsync(int iCodUsuario)
        {
            var lista = new List<IdiomaDTO>();
            string mensaje = string.Empty;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_Idioma_Listar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);

                SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pMensaje);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new IdiomaDTO
                        {
                            iCodIdioma = reader.GetInt32(reader.GetOrdinal("iCodIdioma")),
                            iCodUsuario = reader.GetInt32(reader.GetOrdinal("iCodUsuario")),
                            vIdioma = reader.GetString(reader.GetOrdinal("vIdioma")),
                            vInstitucion = reader.GetString(reader.GetOrdinal("vInstitucion")),
                            vNivelAlcanzado = reader.GetString(reader.GetOrdinal("vNivelAlcanzado")),
                            dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                            iCodUsuarioRegistra = reader.GetInt32(reader.GetOrdinal("iCodUsuarioRegistra")),
                            bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                        });
                    }
                }

                mensaje = pMensaje.Value?.ToString() ?? string.Empty;
            }

            return lista;
        }

        public async Task<string> ActualizarAsync(IdiomaDTO entidad)
        {
            string mensaje = string.Empty;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_Idioma_Actualizar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodIdioma", entidad.iCodIdioma);
                cmd.Parameters.AddWithValue("@vIdioma", entidad.vIdioma);
                cmd.Parameters.AddWithValue("@vInstitucion", entidad.vInstitucion);
                cmd.Parameters.AddWithValue("@vNivelAlcanzado", entidad.vNivelAlcanzado);

                SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pMensaje);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                mensaje = pMensaje.Value.ToString();
            }

            return mensaje;
        }

        public async Task<string> EliminarAsync(int iCodIdioma)
        {
            string mensaje = string.Empty;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_Idioma_Eliminar", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodIdioma", iCodIdioma);

                SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pMensaje);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                mensaje = pMensaje.Value.ToString();
            }

            return mensaje;
        }
    }
}
