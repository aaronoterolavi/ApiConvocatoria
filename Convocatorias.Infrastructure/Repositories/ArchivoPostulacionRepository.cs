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
    public class ArchivoPostulacionRepository : IArchivoPostulacionRepository
    {
        private readonly string _connectionString;
        private readonly string _baseUrl;
        private readonly string _requestPath;

        public ArchivoPostulacionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("DefaultConnection no configurada");
            _baseUrl = configuration["Archivos:BaseUrl"] ?? string.Empty;
            _requestPath = configuration["LocalStorage:RequestPath"] ?? string.Empty;
        }

        public async Task<ArchivoPostulacionResponseDto> InsertarAsync(ArchivoPostulacionInsertarDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_InsertarArchivoPostulacion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodPostulacion", dto.iCodPostulacion);
            cmd.Parameters.AddWithValue("@iCodFormato", dto.iCodFormato);
            cmd.Parameters.AddWithValue("@vRutaArchivo", dto.vRutaArchivo);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(mensajeParam);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return new ArchivoPostulacionResponseDto
            {
                Mensaje = mensajeParam.Value?.ToString()
            };
        }

        public async Task<List<ArchivoPostulacionDto>> ListarAsync()
        {
            var lista = new List<ArchivoPostulacionDto>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("PA_ListarArchivosPostulacion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new ArchivoPostulacionDto
                {
                    iCodArchivo = reader.GetInt32(reader.GetOrdinal("iCodArchivo")),
                    iCodPostulacion = reader.GetInt32(reader.GetOrdinal("iCodPostulacion")),
                    iCodFormato = reader.GetInt32(reader.GetOrdinal("iCodFormato")),
                    vRutaArchivo = reader.GetString(reader.GetOrdinal("vRutaArchivo")),
                    iCodUsuarioRegistra = reader.GetInt32(reader.GetOrdinal("iCodUsuarioRegistra")),
                    dtFechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                    bActivo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                });
            }

            return lista;
        }

        public async Task<IEnumerable<ArchivoPostulacionListadoDto>> ListarPorPostulacionAsync(
     
     int? iCodConvocatoria = null,
     int? iCodUsuario = null)
        {
            var archivos = new List<ArchivoPostulacionListadoDto>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_ArchivosPostulacion_ListarPorPostulacion", conn);
            cmd.CommandType = CommandType.StoredProcedure;

           
            // Parámetros opcionales
           
            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria.HasValue ? iCodConvocatoria.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario.HasValue ? iCodUsuario.Value : DBNull.Value);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var rutaArchivo = reader["vRutaArchivo"].ToString() ?? string.Empty;
                rutaArchivo = rutaArchivo.Replace("\\", "/");

                // 1️⃣ Obtener el nombre del archivo con extensión
                string archivo = Path.GetFileName(rutaArchivo);

                // 2️⃣ Quitar extensión
                string sinExtension = Path.GetFileNameWithoutExtension(archivo);

                // 3️⃣ Tomar la parte antes del primer "_"
                string nombreBase = sinExtension.Split('_')[0];

                archivos.Add(new ArchivoPostulacionListadoDto
                {
                    iCodArchivo = reader["iCodArchivo"] != DBNull.Value ? Convert.ToInt32(reader["iCodArchivo"]) : 0,
                    iCodPostulacion = reader["iCodPostulacion"] != DBNull.Value ? Convert.ToInt32(reader["iCodPostulacion"]) : 0,
                    iCodConvocatoria = reader["iCodConvocatoria"] != DBNull.Value ? Convert.ToInt32(reader["iCodConvocatoria"]) : 0,
                    iCodUsuario = reader["iCodUsuario"] != DBNull.Value ? Convert.ToInt32(reader["iCodUsuario"]) : 0,
                    iCodFormato = reader["iCodFormato"] != DBNull.Value ? Convert.ToInt32(reader["iCodFormato"]) : 0,
                    vDescFormato = reader["vDescFormato"].ToString() ?? string.Empty,
                    vRutaArchivo = rutaArchivo,
                    iCodUsuarioRegistra = reader["iCodUsuarioRegistra"] != DBNull.Value ? Convert.ToInt32(reader["iCodUsuarioRegistra"]) : 0,
                    dtFechaRegistro = reader["dtFechaRegistro"] != DBNull.Value ? Convert.ToDateTime(reader["dtFechaRegistro"]) : DateTime.MinValue,
                    bActivo = reader["bActivo"] != DBNull.Value && Convert.ToBoolean(reader["bActivo"]),
                    vNombreArchivo = nombreBase,

                    // 🌐 URL pública completa → https://localhost:7106/UGRHCONVOCATORIA/postulacion/15/archivo.pdf
                    UrlArchivo = $"{_baseUrl.TrimEnd('/')}{_requestPath}{rutaArchivo}"
                });
            }

            return archivos;
        }


    }
}
