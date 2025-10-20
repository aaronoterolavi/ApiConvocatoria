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
    public class FichaCurricularRepository : IFichaCurricularRepository
    {
        private readonly string _connectionString;

        public FichaCurricularRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<FichaCurricularDto> ObtenerFichaCurricularAsync(int iCodUsuario)
        {
            var dto = new FichaCurricularDto();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("PA_ObtenerFichaResumenCurricular", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    // 1️⃣ Datos personales
                    if (await reader.ReadAsync())
                    {
                        dto.DatosPersonales = new DatosPersonalesDto
                        {
                            vNumDocumento = reader["vNumDocumento"].ToString(),
                            vApePaterno = reader["vApePaterno"].ToString(),
                            vApeMaterno = reader["vApeMaterno"].ToString(),
                            vNombres = reader["vNombres"].ToString(),
                            vCorreoElectronico = reader["vCorreoElectronico"].ToString(),
                            vCodigoPostulacion = reader["vCodigoPostulacion"].ToString(),
                            dFechaNacimiento = reader["dFechaNacimiento"] as DateTime?,
                            vSexo = reader["vSexo"].ToString(),
                            vEstadoCivil = reader["vEstadoCivil"].ToString(),
                            vNomDepartamento = reader["vNomDepartamento"].ToString(),
                            vNomProvincia = reader["vNomProvincia"].ToString(),
                            vNomDistrito = reader["vNomDistrito"].ToString(),
                            vDomicilio = reader["vDomicilio"].ToString(),
                            vCelular = reader["vCelular"].ToString(),
                            vTelefono = reader["vTelefono"].ToString(),
                            vNroColegiatura = reader["vNroColegiatura"].ToString(),
                            vHabilitado = reader["vHabilitado"].ToString(),
                            vLicenciaFFAA = reader["vLicenciaFFAA"].ToString(),
                            vNroCarnetFFAA = reader["vNroCarnetFFAA"].ToString(),
                            vDiscapacidad = reader["vDiscapacidad"].ToString(),
                            vNroCarnetDiscapacidad = reader["vNroCarnetDiscapacidad"].ToString()
                        };
                    }

                    // 2️⃣ Formacion
                    await reader.NextResultAsync();
                    while (await reader.ReadAsync())
                        dto.Formaciones.Add(new FormacionAcademicaDto
                        {
                            vNivelAcademico = reader["vNivelAcademico"].ToString(),
                            vInstitucion = reader["vInstitucion"].ToString(),
                            vProfesion = reader["vProfesion"].ToString(),
                            dFechaEgreso = reader["dFechaEgreso"] as DateTime?
                        });

                    // 3️⃣ Cursos
                    await reader.NextResultAsync();
                    while (await reader.ReadAsync())
                        dto.Cursos.Add(new CursoDiplomadoDto
                        {
                            vCurso = reader["vCurso"].ToString(),
                            vNombreInstitucion = reader["vNombreInstitucion"].ToString(),
                            iHoras = Convert.ToInt32(reader["iHoras"])
                        });

                    // 4️⃣ Experiencias
                    await reader.NextResultAsync();
                    while (await reader.ReadAsync())
                        dto.Experiencias.Add(new ExperienciaLaboral2Dto
                        {
                            vEntidad = reader["vEntidad"].ToString(),
                            vUnidadOrganica = reader["vUnidadOrganica"].ToString(),
                            vCargo = reader["vCargo"].ToString(),
                            dFechaInicio = reader["dFechaInicio"] as DateTime?,
                            dFechaFin = reader["dFechaFin"] as DateTime?,
                            vFunciones = reader["vFunciones"].ToString(),
                            vActAgricolas = reader["vActAgricolas"].ToString(),
                            vActAgropecuarias = reader["vActAgropecuarias"].ToString(),
                            vTemasSanitarios = reader["vTemasSanitarios"].ToString(),
                            vAccesoMercadosExternos = reader["vAccesoMercadosExternos"].ToString()
                        });

                    // 5️⃣ Idiomas
                    await reader.NextResultAsync();
                    while (await reader.ReadAsync())
                        dto.Idiomas.Add(new IdiomaDto
                        {
                            vIdioma = reader["vIdioma"].ToString(),
                            vInstitucion = reader["vInstitucion"].ToString(),
                            vNivelAlcanzado = reader["vNivelAlcanzado"].ToString()
                        });

                    // 6️⃣ Ofimática
                    await reader.NextResultAsync();
                    if (await reader.ReadAsync())
                        dto.ConocimientoOfimatica = reader["vTieneConocimiento"].ToString();

                    // 7️⃣ Declaración
                    await reader.NextResultAsync();
                    if (await reader.ReadAsync())
                        dto.DeclaracionJurada = new DeclaracionJuradaDto
                        {
                            vSinAntecedentesPenales = reader["vSinAntecedentesPenales"].ToString(),
                            vSinProcesosJudiciales = reader["vSinProcesosJudiciales"].ToString(),
                            vSinSancionesAdministrativas = reader["vSinSancionesAdministrativas"].ToString(),
                            vSinVinculoLaboralEstado = reader["vSinVinculoLaboralEstado"].ToString(),
                            vAceptaBasesConcurso = reader["vAceptaBasesConcurso"].ToString()
                        };
                }
            }

            return dto;
        }
    }
}
