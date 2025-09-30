using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Convocatorias.Infrastructure.Storage
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Sube un archivo al servidor FTP y devuelve la ruta remota (por ejemplo: /convocatorias/2025/archivo.pdf).
        /// </summary>
        /// <param name="file">Archivo recibido desde el cliente</param>
        /// <param name="remoteFolder">Carpeta remota (p.ej. "convocatorias/123")</param>
        Task<string> UploadFileAsync(IFormFile file, string remoteFolder);

        /// <summary>
        /// Opción: nombre único para almacenar (timestamp+guid)
        /// </summary>
        string GenerateUniqueFileName(string originalFileName);
    }
}
