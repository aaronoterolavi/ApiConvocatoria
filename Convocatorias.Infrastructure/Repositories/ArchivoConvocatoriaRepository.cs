using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class ArchivoConvocatoriaRepository : IArchivoConvocatoriaRepository
    {
        private readonly string _connectionString;
        private readonly string _baseUrl;
        private readonly string _requestPath;

        public ArchivoConvocatoriaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
         ?? throw new ArgumentNullException("DefaultConnection no configurada");
            _baseUrl = config["Archivos:BaseUrl"] ?? string.Empty;
            _requestPath = config["LocalStorage:RequestPath"] ?? string.Empty;
        }

        public async Task<ArchivoConvocatoriaResponseDto> InsertarAsync(ArchivoConvocatoriaInsertarDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_ArchivosConvocatoria_Insertar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vRutaArchivo", dto.RutaArchivo);
            cmd.Parameters.AddWithValue("@iCodConvocatoria", dto.CodConvocatoria);
            cmd.Parameters.AddWithValue("@iCodFormato", dto.CodFormato);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.CodUsuarioRegistra);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ArchivoConvocatoriaResponseDto
                {
                    NuevoId = reader["NuevoId"] != DBNull.Value ? Convert.ToInt32(reader["NuevoId"]) : 0,
                    Mensaje = reader["Mensaje"].ToString() ?? string.Empty,
                    Codigo = reader["Codigo"] != DBNull.Value ? Convert.ToInt32(reader["Codigo"]) : 0
                };
            }

            return new ArchivoConvocatoriaResponseDto
            {
                NuevoId = 0,
                Mensaje = "Error al registrar archivo.",
                Codigo = 0
            };
        }

        public async Task<IEnumerable<ArchivoConvocatoriaDto>> ListarPorConvocatoriaAsync(
   int iCodConvocatoria,
   int? iCodFormato = null)
        {
            var archivos = new List<ArchivoConvocatoriaDto>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_ArchivosConvocatoria_ListarPorConvocatoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCodConvocatoria", iCodConvocatoria);

            if (iCodFormato.HasValue)
                cmd.Parameters.AddWithValue("@iCodFormato", iCodFormato.Value);
            else
                cmd.Parameters.AddWithValue("@iCodFormato", DBNull.Value);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var rutaArchivo = reader["vRutaArchivo"].ToString() ?? string.Empty;

                // 🔹 Normalizar separadores
                rutaArchivo = rutaArchivo.Replace("\\", "/");

                // 1. Obtener solo el nombre del archivo con extensión
                string archivo = Path.GetFileName(rutaArchivo);

                // 2. Quitar la extensión
                string sinExtension = Path.GetFileNameWithoutExtension(archivo);

                // 3. Tomar la parte antes del primer "_"
                string resultado = sinExtension.Split('_')[0];

                archivos.Add(new ArchivoConvocatoriaDto
                {
                    iCodAdjunto = reader["iCodAdjunto"] != DBNull.Value ? Convert.ToInt32(reader["iCodAdjunto"]) : 0,
                    vRutaArchivo = rutaArchivo,
                    iCodConvocatoria = reader["iCodConvocatoria"] != DBNull.Value ? Convert.ToInt32(reader["iCodConvocatoria"]) : 0,
                    iCodFormato = reader["iCodFormato"] != DBNull.Value ? Convert.ToInt32(reader["iCodFormato"]) : 0,
                    vDescFormato = reader["vDescFormato"].ToString() ?? string.Empty,
                    iCodUsuarioRegistra = reader["iCodUsuarioRegistra"] != DBNull.Value ? Convert.ToInt32(reader["iCodUsuarioRegistra"]) : 0,
                    dtFechaRegistro = reader["dtFechaRegistro"] != DBNull.Value ? Convert.ToDateTime(reader["dtFechaRegistro"]) : DateTime.MinValue,
                    bActivo = reader["bActivo"] != DBNull.Value && Convert.ToBoolean(reader["bActivo"]),
                    vNombreArchivo = resultado,

                    // ✅ URL pública con carpeta "convocatoria"
                    UrlArchivo = $"{_baseUrl.TrimEnd('/')}{_requestPath}{rutaArchivo}"

                });
            }

            return archivos;
        }


        public async Task<EliminarArchivoResponseDto> EliminarArchivoAsync(int idAdjunto)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_ArchivosConvocatoria_Eliminar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iCodAdjunto", idAdjunto);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new EliminarArchivoResponseDto
                        {
                            IdEliminado = reader["IdEliminado"] != DBNull.Value ? (int)reader["IdEliminado"] : 0,
                            Mensaje = reader["Mensaje"].ToString(),
                            Codigo = reader["Codigo"] != DBNull.Value ? (int)reader["Codigo"] : 0
                        };
                    }
                }
            }

            return new EliminarArchivoResponseDto
            {
                IdEliminado = 0,
                Mensaje = "No se pudo eliminar el archivo",
                Codigo = 0
            };
        }


    }
}
