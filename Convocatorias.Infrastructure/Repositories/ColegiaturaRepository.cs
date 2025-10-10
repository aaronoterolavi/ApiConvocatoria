using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class ColegiaturaRepository : IColegiaturaRepository
    {
        private readonly string _connectionString;

        public ColegiaturaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public int Insertar(ColegiaturaDto dto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_InsertarColegiatura", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
                cmd.Parameters.AddWithValue("@iCodColegioProfesional", dto.iCodColegioProfesional);
                cmd.Parameters.AddWithValue("@vNroColegiatura", dto.vNroColegiatura);
                cmd.Parameters.AddWithValue("@bHabilitado", dto.bHabilitado);
                cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);
                cmd.Parameters.AddWithValue("@bActivo", dto.bActivo);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public void Actualizar(ColegiaturaDto dto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_ActualizarColegiatura", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodColegiatura", dto.iCodColegiatura);
                cmd.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
                cmd.Parameters.AddWithValue("@iCodColegioProfesional", dto.iCodColegioProfesional);
                cmd.Parameters.AddWithValue("@vNroColegiatura", dto.vNroColegiatura);
                cmd.Parameters.AddWithValue("@bHabilitado", dto.bHabilitado);
                cmd.Parameters.AddWithValue("@bActivo", dto.bActivo);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int iCodColegiatura)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_EliminarColegiatura", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodColegiatura", iCodColegiatura);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ColegiaturaDto> Listar()
        {
            var lista = new List<ColegiaturaDto>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_ListarColegiatura", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ColegiaturaDto
                        {
                            iCodColegiatura = Convert.ToInt32(dr["iCodColegiatura"]),
                            iCodUsuario = Convert.ToInt32(dr["iCodUsuario"]),
                            iCodColegioProfesional = Convert.ToInt32(dr["iCodColegioProfesional"]),
                            vNroColegiatura = dr["vNroColegiatura"].ToString()!,
                            bHabilitado = Convert.ToBoolean(dr["bHabilitado"]),
                            iCodUsuarioRegistra = Convert.ToInt32(dr["iCodUsuarioRegistra"]),
                            dtFechaRegistro = Convert.ToDateTime(dr["dtFechaRegistro"]),
                            bActivo = Convert.ToBoolean(dr["bActivo"])
                        });
                    }
                }
            }
            return lista;
        }

        public List<ColegiaturaDto> ListarPorUsuario(int iCodUsuario)
        {
            var lista = new List<ColegiaturaDto>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PA_ListarColegiaturaPorUsuario", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ColegiaturaDto
                        {
                            iCodColegiatura = Convert.ToInt32(dr["iCodColegiatura"]),
                            iCodUsuario = Convert.ToInt32(dr["iCodUsuario"]),
                            iCodColegioProfesional = Convert.ToInt32(dr["iCodColegioProfesional"]),
                            vNroColegiatura = dr["vNroColegiatura"].ToString()!,
                            bHabilitado = Convert.ToBoolean(dr["bHabilitado"]),
                            iCodUsuarioRegistra = Convert.ToInt32(dr["iCodUsuarioRegistra"]),
                            dtFechaRegistro = Convert.ToDateTime(dr["dtFechaRegistro"]),
                            bActivo = Convert.ToBoolean(dr["bActivo"])
                        });
                    }
                }
            }
            return lista;
        }
    }
}
