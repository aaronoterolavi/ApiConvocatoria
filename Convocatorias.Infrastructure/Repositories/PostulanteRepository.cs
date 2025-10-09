using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class PostulanteRepository : IPostulanteRepository
    {
        private readonly string _connectionString;

        public PostulanteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<PostulanteDto>> ListarAsync()
        {
            var lista = new List<PostulanteDto>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("usp_Postulante_Listar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new PostulanteDto
                        {
                            CodPostulante = reader.GetInt32(reader.GetOrdinal("iCodPostulante")),
                            CodUsuario = reader.GetInt32(reader.GetOrdinal("iCodUsuario")),
                            CodigoPostulacion = reader["vCodigoPostulacion"] as string,
                            FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("dFechaNacimiento")) ? null : reader.GetDateTime(reader.GetOrdinal("dFechaNacimiento")),
                            CodSexo = reader.IsDBNull(reader.GetOrdinal("iCodSexo")) ? null : reader.GetInt32(reader.GetOrdinal("iCodSexo")),
                            CodEstadoCivil = reader.IsDBNull(reader.GetOrdinal("iCodEstadoCivil")) ? null : reader.GetInt32(reader.GetOrdinal("iCodEstadoCivil")),
                            CodDepartamento = reader.IsDBNull(reader.GetOrdinal("iCodDepartamento")) ? null : reader.GetInt32(reader.GetOrdinal("iCodDepartamento")),
                            CodProvincia = reader.IsDBNull(reader.GetOrdinal("iCodProvincia")) ? null : reader.GetInt32(reader.GetOrdinal("iCodProvincia")),
                            CodDistrito = reader.IsDBNull(reader.GetOrdinal("iCodDistrito")) ? null : reader.GetInt32(reader.GetOrdinal("iCodDistrito")),
                            Domicilio = reader["vDomicilio"] as string,
                            Celular = reader["vCelular"] as string,
                            Telefono = reader["vTelefono"] as string,
                            Correo = reader["vCorreo"] as string,
                            FechaRegistro = reader.IsDBNull(reader.GetOrdinal("dtFechaRegistro")) ? null : reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                            Activo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                        });
                    }
                }
            }

            return lista;
        }

        public async Task<PostulanteDto?> ObtenerPorIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("usp_Postulante_ObtenerPorId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iCodPostulante", id);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new PostulanteDto
                        {
                            CodPostulante = reader.GetInt32(reader.GetOrdinal("iCodPostulante")),
                            CodUsuario = reader.GetInt32(reader.GetOrdinal("iCodUsuario")),
                            CodigoPostulacion = reader["vCodigoPostulacion"] as string,
                            FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("dFechaNacimiento")) ? null : reader.GetDateTime(reader.GetOrdinal("dFechaNacimiento")),
                            CodSexo = reader.IsDBNull(reader.GetOrdinal("iCodSexo")) ? null : reader.GetInt32(reader.GetOrdinal("iCodSexo")),
                            CodEstadoCivil = reader.IsDBNull(reader.GetOrdinal("iCodEstadoCivil")) ? null : reader.GetInt32(reader.GetOrdinal("iCodEstadoCivil")),
                            CodDepartamento = reader.IsDBNull(reader.GetOrdinal("iCodDepartamento")) ? null : reader.GetInt32(reader.GetOrdinal("iCodDepartamento")),
                            CodProvincia = reader.IsDBNull(reader.GetOrdinal("iCodProvincia")) ? null : reader.GetInt32(reader.GetOrdinal("iCodProvincia")),
                            CodDistrito = reader.IsDBNull(reader.GetOrdinal("iCodDistrito")) ? null : reader.GetInt32(reader.GetOrdinal("iCodDistrito")),
                            Domicilio = reader["vDomicilio"] as string,
                            Celular = reader["vCelular"] as string,
                            Telefono = reader["vTelefono"] as string,
                            Correo = reader["vCorreo"] as string,
                            FechaRegistro = reader.IsDBNull(reader.GetOrdinal("dtFechaRegistro")) ? null : reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                            Activo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task<string> InsertarAsync(PostulanteDto postulante)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("usp_Postulante_Insertar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@iCodUsuario", postulante.CodUsuario);
                command.Parameters.AddWithValue("@vCodigoPostulacion", (object?)postulante.CodigoPostulacion ?? DBNull.Value);
                command.Parameters.AddWithValue("@dFechaNacimiento", (object?)postulante.FechaNacimiento ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodSexo", (object?)postulante.CodSexo ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodEstadoCivil", (object?)postulante.CodEstadoCivil ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodDepartamento", (object?)postulante.CodDepartamento ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodProvincia", (object?)postulante.CodProvincia ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodDistrito", (object?)postulante.CodDistrito ?? DBNull.Value);
                command.Parameters.AddWithValue("@vDomicilio", (object?)postulante.Domicilio ?? DBNull.Value);
                command.Parameters.AddWithValue("@vCelular", (object?)postulante.Celular ?? DBNull.Value);
                command.Parameters.AddWithValue("@vTelefono", (object?)postulante.Telefono ?? DBNull.Value);
                command.Parameters.AddWithValue("@vCorreo", (object?)postulante.Correo ?? DBNull.Value);
                command.Parameters.AddWithValue("@bActivo", 1);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // El SP devuelve iCodPostulante y Mensaje
                        var mensaje = reader["Mensaje"]?.ToString() ?? "Postulante registrado.";
                        return mensaje;
                    }
                }
            }

            return "No se pudo insertar el postulante.";
        }

        public async Task<string> ActualizarAsync(PostulanteDto postulante)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("usp_Postulante_Actualizar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@iCodPostulante", postulante.CodPostulante);
                command.Parameters.AddWithValue("@iCodUsuario", postulante.CodUsuario);
                command.Parameters.AddWithValue("@vCodigoPostulacion", (object?)postulante.CodigoPostulacion ?? DBNull.Value);
                command.Parameters.AddWithValue("@dFechaNacimiento", (object?)postulante.FechaNacimiento ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodSexo", (object?)postulante.CodSexo ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodEstadoCivil", (object?)postulante.CodEstadoCivil ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodDepartamento", (object?)postulante.CodDepartamento ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodProvincia", (object?)postulante.CodProvincia ?? DBNull.Value);
                command.Parameters.AddWithValue("@iCodDistrito", (object?)postulante.CodDistrito ?? DBNull.Value);
                command.Parameters.AddWithValue("@vDomicilio", (object?)postulante.Domicilio ?? DBNull.Value);
                command.Parameters.AddWithValue("@vCelular", (object?)postulante.Celular ?? DBNull.Value);
                command.Parameters.AddWithValue("@vTelefono", (object?)postulante.Telefono ?? DBNull.Value);
                command.Parameters.AddWithValue("@vCorreo", (object?)postulante.Correo ?? DBNull.Value);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var mensaje = reader["Mensaje"]?.ToString() ?? "Postulante actualizado correctamente.";
                        return mensaje;
                    }
                }
            }

            return "No se pudo actualizar el postulante.";
        }

        public async Task<string> EliminarAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("usp_Postulante_Eliminar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iCodPostulante", id);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var mensaje = reader[0]?.ToString() ?? "Postulante eliminado correctamente.";
                        return mensaje;
                    }
                }
            }

            return "No se pudo eliminar el postulante.";
        }
    }
}
