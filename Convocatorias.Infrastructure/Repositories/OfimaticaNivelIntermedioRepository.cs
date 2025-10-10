using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class OfimaticaNivelIntermedioRepository : IOfimaticaNivelIntermedioRepository
    {
        private readonly string _connectionString;

        public OfimaticaNivelIntermedioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string Insertar(OfimaticaNivelIntermedioDTO dto)
        {
            string mensaje = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("PA_InsertarOfimaticaNivelIntermedio", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
                cmd.Parameters.AddWithValue("@bTieneConocimiento", dto.bTieneConocimiento);
                cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();

                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }

            return mensaje;
        }

        public string Actualizar(OfimaticaNivelIntermedioDTO dto)
        {
            string mensaje = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("PA_ActualizarOfimaticaNivelIntermedio", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodOfimaticaNivelIntermedio", dto.iCodOfimaticaNivelIntermedio);
                cmd.Parameters.AddWithValue("@bTieneConocimiento", dto.bTieneConocimiento);
                cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();

                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }

            return mensaje;
        }

        public string Eliminar(int iCodOfimaticaNivelIntermedio)
        {
            string mensaje = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("PA_EliminarOfimaticaNivelIntermedio", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodOfimaticaNivelIntermedio", iCodOfimaticaNivelIntermedio);
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();

                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }

            return mensaje;
        }

        public (List<OfimaticaNivelIntermedioDTO> lista, string mensaje) Listar(int? iCodUsuario = null)
        {
            var lista = new List<OfimaticaNivelIntermedioDTO>();
            string mensaje = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("PA_ListarOfimaticaNivelIntermedio", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodUsuario", (object?)iCodUsuario ?? DBNull.Value);
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new OfimaticaNivelIntermedioDTO
                        {
                            iCodOfimaticaNivelIntermedio = Convert.ToInt32(reader["iCodOfimaticaNivelIntermedio"]),
                            iCodUsuario = Convert.ToInt32(reader["iCodUsuario"]),
                            bTieneConocimiento = Convert.ToBoolean(reader["bTieneConocimiento"]),
                            dtFechaRegistro = Convert.ToDateTime(reader["dtFechaRegistro"]),
                            iCodUsuarioRegistra = Convert.ToInt32(reader["iCodUsuarioRegistra"]),
                            bActivo = Convert.ToBoolean(reader["bActivo"])
                        });
                    }
                }

                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }

            return (lista, mensaje);
        }
    }
}
