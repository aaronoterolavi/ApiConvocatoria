using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Convocatorias.Infrastructure.Storage;

namespace Convocatorias.Application.Services
{
    public class ArchivoPostulacionService
    {
        private readonly IArchivoPostulacionRepository _repo;
        private readonly IFileStorageService _fileStorage;

        public ArchivoPostulacionService(IArchivoPostulacionRepository repo, IFileStorageService fileStorage)
        {
            _repo = repo;
            _fileStorage = fileStorage;
        }

        public async Task<List<ArchivoPostulacionResponseDto>> SubirYRegistrarArchivosAsync(
            List<ArchivoPostulacionUploadDto> archivos,
            int codPostulacion,
            int codUsuario)
        {
            var resultados = new List<ArchivoPostulacionResponseDto>();

            foreach (var item in archivos)
            {
                // 📁 Carpeta "postulacion/{id}"
                string remoteFolder = $"postulacion/{codPostulacion}";

                // 🟢 Subir archivo (local o FTP según configuración)
                string rutaArchivo = await _fileStorage.UploadFileAsync(item.File, remoteFolder);

                // 💾 Registrar en BD
                var dto = new ArchivoPostulacionInsertarDto
                {
                    iCodPostulacion = codPostulacion,
                    iCodFormato = item.iCodFormato,
                    vRutaArchivo = rutaArchivo,
                    iCodUsuarioRegistra = codUsuario
                };

                var result = await _repo.InsertarAsync(dto);
                resultados.Add(result);
            }

            return resultados;
        }

        public async Task<IEnumerable<ArchivoPostulacionListadoDto>> ListarPorPostulacionAsync(  int? iCodConvocatoria = null,
     int? iCodUsuario = null)
        {
            return await _repo.ListarPorPostulacionAsync(  iCodConvocatoria, iCodUsuario);
        }

        public async Task<List<ArchivoPostulacionDto>> ListarAsync()
        {
            return await _repo.ListarAsync();
        }
    }
}
