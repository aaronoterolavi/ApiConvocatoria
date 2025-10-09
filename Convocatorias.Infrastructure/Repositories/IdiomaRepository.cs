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

        public int Insert(IdiomaDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("USP_Idioma_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodPostulante", dto.iCodPostulante);
            command.Parameters.AddWithValue("@vIdioma", dto.vIdioma);
            command.Parameters.AddWithValue("@vInstitucion", dto.vInstitucion);
            command.Parameters.AddWithValue("@vNivelAlcanzado", dto.vNivelAlcanzado);
            command.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            connection.Open();
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public void Update(IdiomaDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("USP_Idioma_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodIdioma", dto.iCodIdioma);
            command.Parameters.AddWithValue("@vIdioma", dto.vIdioma);
            command.Parameters.AddWithValue("@vInstitucion", dto.vInstitucion);
            command.Parameters.AddWithValue("@vNivelAlcanzado", dto.vNivelAlcanzado);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int iCodIdioma)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("USP_Idioma_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodIdioma", iCodIdioma);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public IdiomaDTO? GetById(int iCodIdioma)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("USP_Idioma_GetById", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodIdioma", iCodIdioma);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new IdiomaDTO
                {
                    iCodIdioma = Convert.ToInt32(reader["iCodIdioma"]),
                    iCodPostulante = Convert.ToInt32(reader["iCodPostulante"]),
                    vIdioma = reader["vIdioma"].ToString()!,
                    vInstitucion = reader["vInstitucion"].ToString()!,
                    vNivelAlcanzado = reader["vNivelAlcanzado"].ToString()!,
                    dtFechaRegistro = Convert.ToDateTime(reader["dtFechaRegistro"]),
                    iCodUsuarioRegistra = Convert.ToInt32(reader["iCodUsuarioRegistra"]),
                    bActivo = Convert.ToBoolean(reader["bActivo"])
                };
            }

            return null;
        }

        public List<IdiomaDTO> GetByPostulante(int iCodPostulante)
        {
            var list = new List<IdiomaDTO>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("USP_Idioma_GetByPostulante", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@iCodPostulante", iCodPostulante);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new IdiomaDTO
                {
                    iCodIdioma = Convert.ToInt32(reader["iCodIdioma"]),
                    iCodPostulante = Convert.ToInt32(reader["iCodPostulante"]),
                    vIdioma = reader["vIdioma"].ToString()!,
                    vInstitucion = reader["vInstitucion"].ToString()!,
                    vNivelAlcanzado = reader["vNivelAlcanzado"].ToString()!,
                    dtFechaRegistro = Convert.ToDateTime(reader["dtFechaRegistro"]),
                    iCodUsuarioRegistra = Convert.ToInt32(reader["iCodUsuarioRegistra"]),
                    bActivo = Convert.ToBoolean(reader["bActivo"])
                });
            }

            return list;
        }
    }
}
