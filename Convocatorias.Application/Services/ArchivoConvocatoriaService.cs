using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Convocatorias.Infrastructure.Storage;

namespace Convocatorias.Application.Services
{
    public class ArchivoConvocatoriaService
    {
        private readonly IArchivoConvocatoriaRepository _repo;
        private readonly IFileStorageService _fileStorage;

        public ArchivoConvocatoriaService(IArchivoConvocatoriaRepository repo, IFileStorageService fileStorage)
        {
            _repo = repo;
            _fileStorage = fileStorage;
        }

        public async Task<List<ArchivoConvocatoriaResponseDto>> SubirYRegistrarArchivosAsync(
       List<ArchivoConvocatoriaUploadDto> archivos,
       int codConvocatoria,
       int codUsuario)
        {
            var resultados = new List<ArchivoConvocatoriaResponseDto>();

            foreach (var item in archivos)
            {
                string remoteFolder = codConvocatoria.ToString();

                // Subir archivo (local/ftp según config)
                string rutaArchivo = await _fileStorage.UploadFileAsync(item.File, remoteFolder);

                // Guardar en BD
                var dto = new ArchivoConvocatoriaInsertarDto
                {
                    RutaArchivo = rutaArchivo,
                    CodConvocatoria = codConvocatoria,
                    CodFormato = item.CodFormato,
                    CodUsuarioRegistra = codUsuario
                };

                var result = await _repo.InsertarAsync(dto);
                resultados.Add(result);
            }

            return resultados;
        }

        public async Task<IEnumerable<ArchivoConvocatoriaDto>> ListarPorConvocatoriaAsync(
    int iCodConvocatoria,
    int? iCodFormato = null)
        {
            return await _repo.ListarPorConvocatoriaAsync(iCodConvocatoria, iCodFormato);
        }

        public async Task<EliminarArchivoResponseDto> EliminarArchivoAsync(int idAdjunto)
        {
            return await _repo.EliminarArchivoAsync(idAdjunto);
        }

    }
}
