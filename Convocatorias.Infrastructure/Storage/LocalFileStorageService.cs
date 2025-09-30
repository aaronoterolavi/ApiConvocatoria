using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public LocalFileStorageService(IConfiguration config)
        {
            // Toma la ruta solo desde configuración
            _basePath = config["LocalStorage:BasePath"]
                ?? throw new ArgumentNullException("LocalStorage:BasePath",
                   "Debes configurar LocalStorage:BasePath en appsettings.json");
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

            // Normalizar remoteFolder
            remoteFolder = remoteFolder.Trim('\\').Trim('/');

            // Carpeta destino en disco
            var folderPath = Path.Combine(_basePath, remoteFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Ruta completa
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retorna ruta relativa (ej. /convocatorias/202509/file.pdf)
            return $"/{remoteFolder}/{fileName}";
        }
    }
}
