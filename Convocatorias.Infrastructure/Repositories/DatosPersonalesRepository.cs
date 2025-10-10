using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class DatosPersonalesRepository : IDatosPersonalesRepository
    {
        private readonly string _connectionString;

        public DatosPersonalesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<DatosPersonalesDTO>> ListarAsync()
        {
            var lista = new List<DatosPersonalesDTO>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_ListarDatosPersonales", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(Map(reader));
            }

            return lista;
        }

        public async Task<DatosPersonalesDTO?> ObtenerPorUsuarioAsync(int iCodUsuario)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_ObtenerDatosPersonalesPorUsuario", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Map(reader);

            return null;
        }

        public async Task InsertarAsync(DatosPersonalesDTO datos)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_InsertarDatosPersonales", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodUsuario", datos.iCodUsuario);
            command.Parameters.AddWithValue("@vCodigoPostulacion", datos.vCodigoPostulacion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@dFechaNacimiento", datos.dFechaNacimiento);
            command.Parameters.AddWithValue("@iCodSexo", datos.iCodSexo);
            command.Parameters.AddWithValue("@iCodEstadoCivil", datos.iCodEstadoCivil);
            command.Parameters.AddWithValue("@vCodDepartamento", datos.vCodDepartamento ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCodProvincia", datos.vCodProvincia ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCodDistrito", datos.vCodDistrito ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vDomicilio", datos.vDomicilio ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCelular", datos.vCelular ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vTelefono", datos.vTelefono ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCorreo", datos.vCorreo ?? (object)DBNull.Value);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsync(DatosPersonalesDTO datos)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_ActualizarDatosPersonales", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodDatosPersonales", datos.iCodDatosPersonales);
            command.Parameters.AddWithValue("@vCodigoPostulacion", datos.vCodigoPostulacion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@dFechaNacimiento", datos.dFechaNacimiento);
            command.Parameters.AddWithValue("@iCodSexo", datos.iCodSexo);
            command.Parameters.AddWithValue("@iCodEstadoCivil", datos.iCodEstadoCivil);
            command.Parameters.AddWithValue("@vCodDepartamento", datos.vCodDepartamento ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCodProvincia", datos.vCodProvincia ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCodDistrito", datos.vCodDistrito ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vDomicilio", datos.vDomicilio ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCelular", datos.vCelular ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vTelefono", datos.vTelefono ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@vCorreo", datos.vCorreo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@bActivo", datos.bActivo);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(int iCodDatosPersonales)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("PA_EliminarDatosPersonales", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@iCodDatosPersonales", iCodDatosPersonales);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        private DatosPersonalesDTO Map(SqlDataReader reader)
        {
            return new DatosPersonalesDTO
            {
                iCodDatosPersonales = reader.GetInt32(reader.GetOrdinal("iCodDatosPersonales")),
                iCodUsuario = reader.GetInt32(reader.GetOrdinal("iCodUsuario")),
                vCodigoPostulacion = reader["vCodigoPostulacion"].ToString(),
                dFechaNacimiento = reader.GetDateTime(reader.GetOrdinal("dFechaNacimiento")),
                iCodSexo = reader.GetInt32(reader.GetOrdinal("iCodSexo")),
                iCodEstadoCivil = reader.GetInt32(reader.GetOrdinal("iCodEstadoCivil")),
                vCodDepartamento = reader["vCodDepartamento"].ToString(),
                vCodProvincia = reader["vCodProvincia"].ToString(),
                vCodDistrito = reader["vCodDistrito"].ToString(),
                vDomicilio = reader["vDomicilio"].ToString(),
                vCelular = reader["vCelular"].ToString(),
                vTelefono = reader["vTelefono"].ToString(),
                vCorreo = reader["vCorreo"].ToString(),
                dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
            };
        }
    }
}
