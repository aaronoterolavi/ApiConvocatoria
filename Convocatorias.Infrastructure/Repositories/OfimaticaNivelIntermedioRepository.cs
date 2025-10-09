using System;
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

        public int Insert(OfimaticaNivelIntermedioDTO dto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_OfimaticaNivelIntermedio_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
                cmd.Parameters.AddWithValue("@bTieneConocimiento", dto.bTieneConocimiento);
                cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public void Update(int iCodOfimaticaNivelIntermedio, bool bTieneConocimiento)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_OfimaticaNivelIntermedio_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodOfimaticaNivelIntermedio", iCodOfimaticaNivelIntermedio);
                cmd.Parameters.AddWithValue("@bTieneConocimiento", bTieneConocimiento);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int iCodOfimaticaNivelIntermedio)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_OfimaticaNivelIntermedio_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodOfimaticaNivelIntermedio", iCodOfimaticaNivelIntermedio);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public OfimaticaNivelIntermedioDTO? GetById(int iCodOfimaticaNivelIntermedio)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_OfimaticaNivelIntermedio_GetById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodOfimaticaNivelIntermedio", iCodOfimaticaNivelIntermedio);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OfimaticaNivelIntermedioDTO
                        {
                            iCodOfimaticaNivelIntermedio = Convert.ToInt32(reader["iCodOfimaticaNivelIntermedio"]),
                            iCodPostulante = Convert.ToInt32(reader["iCodPostulante"]),
                            bTieneConocimiento = Convert.ToBoolean(reader["bTieneConocimiento"]),
                            dtFechaRegistro = Convert.ToDateTime(reader["dtFechaRegistro"]),
                            iCodUsuarioRegistra = Convert.ToInt32(reader["iCodUsuarioRegistra"]),
                            bActivo = Convert.ToBoolean(reader["bActivo"])
                        };
                    }
                }
            }
            return null;
        }

        public OfimaticaNivelIntermedioDTO? GetByPostulante(int iCodPostulante)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_OfimaticaNivelIntermedio_GetByPostulante", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodPostulante", iCodPostulante);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OfimaticaNivelIntermedioDTO
                        {
                            iCodOfimaticaNivelIntermedio = Convert.ToInt32(reader["iCodOfimaticaNivelIntermedio"]),
                            iCodPostulante = Convert.ToInt32(reader["iCodPostulante"]),
                            bTieneConocimiento = Convert.ToBoolean(reader["bTieneConocimiento"]),
                            dtFechaRegistro = Convert.ToDateTime(reader["dtFechaRegistro"]),
                            iCodUsuarioRegistra = Convert.ToInt32(reader["iCodUsuarioRegistra"]),
                            bActivo = Convert.ToBoolean(reader["bActivo"])
                        };
                    }
                }
            }
            return null;
        }
    }
}
