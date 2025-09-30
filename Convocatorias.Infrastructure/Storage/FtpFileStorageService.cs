using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Storage
{
    public class FtpFileStorageService : IFileStorageService
    {
        private readonly string _ftpBaseUrl;
        private readonly string _ftpUser;
        private readonly string _ftpPassword;

        public FtpFileStorageService(IConfiguration config)
        {
            _ftpBaseUrl = config["FtpStorage:BaseUrl"] ?? "ftp://localhost/convocatorias";
            _ftpUser = config["FtpStorage:User"];
            _ftpPassword = config["FtpStorage:Password"];
        }

        public string GenerateUniqueFileName(string originalFileName)
        {
            var ext = Path.GetExtension(originalFileName);
            var name = Path.GetFileNameWithoutExtension(originalFileName);
            var unique = $"{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}";
            return $"{name}_{unique}{ext}";
        }

        public async Task<string> UploadFileAsync(IFormFile file, string remoteFolder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("El archivo es nulo o vacío.", nameof(file));

            var fileName = GenerateUniqueFileName(file.FileName);

            // Normalizar remoteFolder → "25"
            remoteFolder = remoteFolder.Trim('/').Trim('\\');

            // Ruta completa en FTP → ftp://.../convocatorias/25/archivo.pdf
            var folderUrl = $"{_ftpBaseUrl.TrimEnd('/')}/{remoteFolder}";
            var fileUrl = $"{folderUrl}/{fileName}";

            // Crear carpeta remota si no existe (FTP no siempre soporta MKD recursivo)
            await EnsureFtpDirectoryExists(folderUrl);

            // Subir archivo
            var request = (FtpWebRequest)WebRequest.Create(fileUrl);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await file.CopyToAsync(requestStream);
            }

            using var response = (FtpWebResponse)await request.GetResponseAsync();

            // Retornar ruta relativa (para BD)
            return $"/{remoteFolder}/{fileName}";
        }

        private async Task EnsureFtpDirectoryExists(string folderUrl)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(folderUrl);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);

                using var response = (FtpWebResponse)await request.GetResponseAsync();
            }
            catch (WebException ex)
            {
                // 550 significa que ya existe la carpeta → se ignora
                if (((FtpWebResponse)ex.Response).StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                    throw;
            }
        }
    }
}
