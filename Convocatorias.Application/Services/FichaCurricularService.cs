using System;
using System.Collections.Generic;
using System.Linq;
 
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Convocatorias.Application.Services
{
    public class FichaCurricularService
    {
       
        private readonly IFichaCurricularRepository _repository;
        
        public FichaCurricularService(IFichaCurricularRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> GenerarPdfAsync(int iCodUsuario)
        {
            var ficha = await _repository.ObtenerFichaCurricularAsync(iCodUsuario);

            using var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {


                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(10));
                    page.Header().PaddingBottom(5).Element(header =>
                    {
                        header.AlignCenter().Image(
                            Path.Combine("Assets", "header_agrorural.png"),
                            ImageScaling.FitWidth // 👈 se ajusta automáticamente al ancho de la hoja A4
                        );
                    });



                    page.Content().PaddingTop(10).Column(col =>
                    {

                        col.Spacing(10);

                        // 🟩 Título del documento
                        col.Item()
                            .AlignCenter()
                            .Text("ANEXO N° 03 - FICHA DE RESUMEN CURRICULAR")
                            .Bold()
                            .FontSize(14)
                            .Underline();
                             


                        // I. DATOS PERSONALES
                        col.Item().PaddingTop(10)
                           .Text("I. DATOS PERSONALES").Bold().FontSize(12);

                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3); // Etiquetas
                                c.RelativeColumn(7); // Valores
                            });

                            // 🟦 Fila: Código Postulación
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("CÓDIGO DE POSTULACIÓN").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3)
                                .Text(ficha.DatosPersonales.vCodigoPostulacion ?? "(no llenar)").FontSize(9);

                            // 🟦 Fila: DNI
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("DNI").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3)
                                .Text(ficha.DatosPersonales.vNumDocumento).FontSize(9);

                            // 🟦 Fila: Apellidos y Nombres
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("APELLIDOS Y NOMBRES").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3)
                                .Text($"{ficha.DatosPersonales.vApePaterno} {ficha.DatosPersonales.vApeMaterno} {ficha.DatosPersonales.vNombres}").FontSize(9);

                            // 🟦 Fila: Fecha Nacimiento, Sexo, Estado Civil
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("FECHA DE NACIMIENTO / SEXO / ESTADO CIVIL").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3).Row(row =>
                            {
                                row.RelativeColumn().Text(ficha.DatosPersonales.dFechaNacimiento?.ToString("dd/MM/yyyy") ?? "").FontSize(9);
                                row.ConstantColumn(40).AlignCenter().Text("SEXO").Bold().FontSize(9);
                                row.ConstantColumn(30).AlignCenter().Text(ficha.DatosPersonales.vSexo == "MASCULINO" ? "M ☑" : "M").FontSize(9);
                                row.ConstantColumn(30).AlignCenter().Text(ficha.DatosPersonales.vSexo == "FEMENINO" ? "F ☑" : "F").FontSize(9);
                                row.RelativeColumn().AlignRight().Text($"ESTADO CIVIL: {ficha.DatosPersonales.vEstadoCivil}").FontSize(9);
                            });

                            // 🟦 Fila: Dirección
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("DIRECCIÓN").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3)
                                .Text(ficha.DatosPersonales.vDomicilio).FontSize(9);

                            // 🟦 Fila: Distrito
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("DISTRITO").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3)
                                .Text($"{ficha.DatosPersonales.vNomDistrito}, {ficha.DatosPersonales.vNomProvincia}, {ficha.DatosPersonales.vNomDepartamento}").FontSize(9);

                            // 🟦 Fila: Teléfono / Móvil
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("TELÉFONO / MÓVIL").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3).Row(r =>
                            {
                                r.RelativeColumn().Text(ficha.DatosPersonales.vTelefono).FontSize(9);
                                r.RelativeColumn().Text(ficha.DatosPersonales.vCelular).FontSize(9);
                            });

                            // 🟦 Fila: Correo
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("CORREO ELECTRÓNICO").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3)
                                .Text(ficha.DatosPersonales.vCorreoElectronico).FontSize(9);

                            // 🟦 Fila: Nº Colegiatura
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("N° DE COLEGIATURA").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3)
                                .Text(ficha.DatosPersonales.vNroColegiatura).FontSize(9);

                            // 🟦 Fila: Habilitado
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("HABILITADO").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3).Row(r =>
                            {
                                r.RelativeColumn().Text(ficha.DatosPersonales.vHabilitado == "SI" ? "SI ☑" : "SI").FontSize(9);
                                r.RelativeColumn().Text(ficha.DatosPersonales.vHabilitado == "NO" ? "NO ☑" : "NO").FontSize(9);
                            });

                            // 🟦 Fila: Licenciado de las FFAA
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("LICENCIADO DE LAS FUERZAS ARMADAS").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3).Row(r =>
                            {
                                r.RelativeColumn().Text(ficha.DatosPersonales.vLicenciaFFAA == "SI" ? "SI ☑" : "SI").FontSize(9);
                                r.RelativeColumn().Text(ficha.DatosPersonales.vLicenciaFFAA == "NO" ? "NO ☑" : "NO").FontSize(9);
                                r.RelativeColumn().Text($"N° CARNET/CÓDIGO: {ficha.DatosPersonales.vNroCarnetFFAA}").FontSize(9);
                            });

                            // 🟦 Fila: Discapacidad
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("TIENE ALGUNA DISCAPACIDAD").Bold().FontSize(9);
                            t.Cell().Border(0.5f).Padding(3).Row(r =>
                            {
                                r.RelativeColumn().Text(ficha.DatosPersonales.vDiscapacidad == "SI" ? "SI ☑" : "SI").FontSize(9);
                                r.RelativeColumn().Text(ficha.DatosPersonales.vDiscapacidad == "NO" ? "NO ☑" : "NO").FontSize(9);
                                r.RelativeColumn().Text($"N° CARNET/CÓDIGO: {ficha.DatosPersonales.vNroCarnetDiscapacidad}").FontSize(9);
                            });

                            // 🟦 Fila: Ajustes razonables
                            t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                .Text("AJUSTES RAZONABLES A LAS PERSONAS CON DISCAPACIDAD").Bold().FontSize(9);

                            t.Cell().Border(0.5f).Padding(3).Column(cc =>
                            {
                                cc.Item().Text("MARCAR CON UNA X EL AJUSTE RAZONABLE REQUERIDO:").Bold().FontSize(9);
                                cc.Item().Text("a) Que las evaluaciones del proceso de selección se efectúen en el primer piso ( )").FontSize(9);
                                cc.Item().Text("b) Ubicarse en las primeras filas donde se realizan las evaluaciones ( )").FontSize(9);
                                cc.Item().Text("c) Apoyo visual, gestual y oral para mejorar la comprensión de las instrucciones ( )").FontSize(9);
                                cc.Item().Text("d) Intérprete de señas durante la evaluación o entrevista personal ( )").FontSize(9);
                                cc.Item().Text("e) Autorizar que el postulante con discapacidad responda o realice preguntas escritas durante la entrevista ( )").FontSize(9);
                            });
                        });



                        // II. FORMACIÓN PROFESIONAL
                        col.Item().PaddingTop(10).Text("II. FORMACIÓN PROFESIONAL").Bold().FontSize(12);

                        col.Item().Table(t =>
                        {
                            // Definir columnas
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3); // Situación académica
                                c.RelativeColumn(3); // Colegio / Universidad
                                c.RelativeColumn(3); // Profesión
                                c.RelativeColumn(2); // Fecha
                                c.RelativeColumn(1); // Folio
                            });

                            // Encabezado
                            t.Header(h =>
                            {
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("SITUACIÓN ACADÉMICA").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("COLEGIO / INSTITUTO / UNIVERSIDAD").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("PROFESIÓN / ESPECIALIDAD").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("FECHA DE EGRESO / BACHILLER / TÍTULO").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("FOLIO (*)").Bold().FontSize(9);
                            });

                            // Lista oficial de niveles académicos
                            string[] niveles = new[]
                            {
                                "Primaria",
                                "Secundaria",
                                "Carrera Técnica",
                                "Egresado Universitario",
                                "Bachiller Universitario",
                                "Título Universitario",
                                "Estudios de Maestría",
                                "Egresado de Maestría",
                                "Grado de Maestría",
                                "Estudios de Doctorado",
                                "Egresado de Doctorado",
                                "Grado de Doctorado"
                            };

                            // Dibujar filas
                            foreach (var nivel in niveles)
                            {
                                var registro = ficha.Formaciones
                                    .FirstOrDefault(f => f.vNivelAcademico.Equals(nivel, StringComparison.OrdinalIgnoreCase));

                                t.Cell().Border(0.5f).Padding(3).Text(nivel);
                                t.Cell().Border(0.5f).Padding(3).Text(registro?.vInstitucion ?? "");
                                t.Cell().Border(0.5f).Padding(3).Text(registro?.vProfesion ?? "");
                                t.Cell().Border(0.5f).Padding(3)
                                    .AlignCenter().Text(registro?.dFechaEgreso?.ToString("dd/MM/yyyy") ?? "");
                                t.Cell().Border(0.5f).Padding(3).AlignCenter().Text("");
                            }
                        });


                        // III. CURSOS Y/O PROGRAMAS DE ESPECIALIZACIÓN Y/O DIPLOMADOS
                        col.Item().PaddingTop(10)
                           .Text("III. CURSOS Y/O PROGRAMAS DE ESPECIALIZACIÓN Y/O DIPLOMADOS")
                           .Bold().FontSize(12);

                        col.Item().Table(t =>
                        {
                            // Definir las columnas
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(5); // Denominación del curso
                                c.RelativeColumn(3); // Nombre de la institución
                                c.RelativeColumn(1); // Horas
                                c.RelativeColumn(1); // Folio
                            });

                            // Encabezado
                            t.Header(h =>
                            {
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter()
                                    .Text("DENOMINACIÓN DEL CURSO Y/O PROGRAMA DE ESPECIALIZACIÓN Y/O DIPLOMADO")
                                    .Bold().FontSize(9);

                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter()
                                    .Text("NOMBRE DE LA INSTITUCIÓN").Bold().FontSize(9);

                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter()
                                    .Text("HORAS").Bold().FontSize(9);

                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter()
                                    .Text("FOLIO (*)").Bold().FontSize(9);
                            });

                            // Número máximo de filas visibles en la tabla (3 como el formato original)
                            int totalFilas = Math.Max(ficha.Cursos.Count, 3);

                            for (int i = 0; i < totalFilas; i++)
                            {
                                var curso = i < ficha.Cursos.Count ? ficha.Cursos[i] : null;

                                t.Cell().Border(0.5f).Padding(3)
                                    .Text(curso?.vCurso ?? "");
                                t.Cell().Border(0.5f).Padding(3)
                                    .Text(curso?.vNombreInstitucion ?? "");
                                t.Cell().Border(0.5f).Padding(3)
                                    .AlignCenter()
                                    .Text(curso?.iHoras > 0 ? curso.iHoras.ToString() : "");
                                t.Cell().Border(0.5f).Padding(3)
                                    .AlignCenter().Text("");
                            }
                        });

                        // IV. EXPERIENCIA LABORAL
                        col.Item().PaddingTop(10).Text("IV. EXPERIENCIA LABORAL").Bold().FontSize(12);

                        foreach (var e in ficha.Experiencias)
                        {
                            col.Item().Border(0.5f).Padding(3).Column(inner =>
                            {
                                // Fila 1: Empresa / Entidad - Folio
                                inner.Item().Table(t =>
                                {
                                    t.ColumnsDefinition(c =>
                                    {
                                        c.RelativeColumn(8);
                                        c.RelativeColumn(2);
                                    });

                                    t.Cell().Border(0.5f).Padding(3).Text($"EMPRESA / ENTIDAD: {e.vEntidad}").Bold();
                                    t.Cell().Border(0.5f).Padding(3).AlignCenter().Text("FOLIO (*)");
                                });

                                // Fila 2: Órgano / Unidad Orgánica
                                inner.Item().Table(t =>
                                {
                                    t.ColumnsDefinition(c => { c.RelativeColumn(10); });
                                    t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                      .Text($"ÓRGANO / UNIDAD ORGÁNICA: {e.vUnidadOrganica}");
                                });

                                // Fila 3: Cargo / Puesto
                                inner.Item().Table(t =>
                                {
                                    t.ColumnsDefinition(c => { c.RelativeColumn(10); });
                                    t.Cell().Border(0.5f).Padding(3)
                                      .Text($"CARGO / PUESTO: {e.vCargo}");
                                });

                                // Fila 4: Público / Privado / Fechas / Total años
                                inner.Item().Table(t =>
                                {
                                    t.ColumnsDefinition(c =>
                                    {
                                        c.RelativeColumn(2); // Público
                                        c.RelativeColumn(2); // Privado
                                        c.RelativeColumn(2); // Fecha Inicio
                                        c.RelativeColumn(2); // Fecha Fin
                                        c.RelativeColumn(2); // Total años
                                    });

                                    t.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter().Text("PÚBLICO").Bold();
                                    t.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter().Text("PRIVADO").Bold();
                                    t.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter().Text("FECHA INICIO").Bold();
                                    t.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter().Text("FECHA FIN").Bold();
                                    t.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter().Text("TOTAL AÑOS").Bold();

                                    t.Cell().Border(0.5f).AlignCenter().Text(e.cSector == "P" ? "X" : "");
                                    t.Cell().Border(0.5f).AlignCenter().Text(e.cSector == "R" ? "X" : "");
                                    t.Cell().Border(0.5f).AlignCenter().Text(e.dFechaInicio?.ToString("dd/MM/yyyy"));
                                    t.Cell().Border(0.5f).AlignCenter().Text(e.dFechaFin?.ToString("dd/MM/yyyy"));
                                    t.Cell().Border(0.5f).AlignCenter().Text(CalcularAnios(e.dFechaInicio, e.dFechaFin));
                                });

                                // Fila 5: Funciones
                                inner.Item().Table(t =>
                                {
                                    t.ColumnsDefinition(c => { c.RelativeColumn(10); });

                                    t.Cell().Background("#D9E1F2").Border(0.5f).Padding(3).Text("FUNCIONES:").Bold();

                                    // lista de funciones separadas por salto de línea
                                    var funciones = e.vFunciones?.Split(new[] { '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
                                    int contador = 1;

                                    if (funciones != null)
                                    {
                                        foreach (var f in funciones)
                                        {
                                            t.Cell().Border(0.5f).PaddingLeft(10)
                                                .Text($"{contador}.- {f.Trim()}").FontSize(9);
                                            contador++;
                                        }
                                    }

                                    // líneas vacías hasta 5
                                    for (; contador <= 5; contador++)
                                        t.Cell().Border(0.5f).PaddingLeft(10).Text($"{contador}.-").FontSize(9);

                                    // línea final (...)
                                    t.Cell().Border(0.5f).PaddingLeft(10).Text("(...)").FontSize(9);
                                });

                                col.Item().PaddingTop(10);
                            });
                        }

                        // VI. CONOCIMIENTO DE OFIMÁTICA A NIVEL INTERMEDIO
                        col.Item().PaddingTop(10)
                           .Text("VI. CONOCIMIENTO DE OFIMÁTICA A NIVEL INTERMEDIO")
                           .Bold().FontSize(12);

                        col.Item().Table(t =>
                        {
                            // Definir las columnas
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(6); // Conocimiento
                                c.RelativeColumn(2); // Señale SI o NO
                            });

                            // Encabezado
                            t.Header(h =>
                            {
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter()
                                    .Text("CONOCIMIENTO")
                                    .Bold().FontSize(9);

                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter()
                                    .Text("SEÑALE\nSI O NO") // Salto de línea
                                    .Bold().FontSize(9);
                            });

                            // Fila principal: "Ofimática a nivel intermedio"
                            t.Cell().Border(0.5f).Padding(3)
                                .Text("OFIMÁTICA A NIVEL INTERMEDIO").FontSize(9);

                            t.Cell().Border(0.5f).Padding(3).Column(column =>
                            {
                                var valor = ficha.ConocimientoOfimatica.Trim().ToUpper() == "SI" ? "SI" : "NO";
                                column.Item().Text(valor == "SI" ? "SI  ☑" : "SI").FontSize(9);
                                column.Item().Text(valor == "NO" ? "NO ☑" : "NO").FontSize(9);
                            });
                        });


                        col.Item().PaddingTop(10);

                        // V. CONOCIMIENTO IDIOMAS ACREDITADO CON CERTIFICADO (NIVEL ALCANZADO)
                        col.Item().PaddingTop(10)
                            .Text("V. CONOCIMIENTO IDIOMAS ACREDITADO CON CERTIFICADO (NIVEL ALCANZADO)")
                            .Bold().FontSize(12);

                        col.Item().Table(t =>
                        {
                            // Definir columnas
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3); // Idioma
                                c.RelativeColumn(3); // Institución
                                c.RelativeColumn(2); // Nivel alcanzado
                                c.RelativeColumn(1); // Folio
                            });

                            // Encabezado
                            t.Header(h =>
                            {
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("IDIOMA").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("INSTITUCIÓN").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("NIVEL ALCANZADO").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).Padding(3)
                                    .AlignCenter().Text("FOLIO (*)").Bold().FontSize(9);
                            });

                            // Filas mínimas (como en el formato oficial)
                            int totalFilas = Math.Max(ficha.Idiomas.Count, 2);

                            for (int i = 0; i < totalFilas; i++)
                            {
                                var idioma = i < ficha.Idiomas.Count ? ficha.Idiomas[i] : null;

                                t.Cell().Border(0.5f).Padding(3)
                                    .Text(idioma?.vIdioma ?? "");
                                t.Cell().Border(0.5f).Padding(3)
                                    .Text(idioma?.vInstitucion ?? "");
                                t.Cell().Border(0.5f).Padding(3)
                                    .Text(idioma?.vNivelAlcanzado ?? "");
                                t.Cell().Border(0.5f).Padding(3)
                                    .AlignCenter().Text("");
                            }
                        });


                        // VII. DECLARACIÓN JURADA
                        col.Item().PaddingTop(10)
                           .Text("VII. DECLARACIÓN JURADA:")
                           .Bold().FontSize(12);

                        col.Item().Table(t =>
                        {
                            // Definición de columnas
                            t.ColumnsDefinition(c =>
                            {
                                c.ConstantColumn(25); // N°
                                c.RelativeColumn(9);  // Otros requisitos
                                c.ConstantColumn(50); // Marcar con (X)
                            });

                            // Encabezado
                            t.Header(h =>
                            {
                                h.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter()
                                    .Text("N°").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter()
                                    .Text("OTROS REQUISITOS").Bold().FontSize(9);
                                h.Cell().Background("#D9E1F2").Border(0.5f).AlignCenter()
                                    .Text("MARCAR CON (X)").Bold().FontSize(9);
                            });

                            // Lista oficial de requisitos
                            var requisitos = new (string Texto, string Valor)[]
                            {
        ("No tener condena por delito doloso, con sentencia firme",
            ficha.DeclaracionJurada.vSinSancionesAdministrativas),
        ("No estar inhabilitado para ejercer la función pública por decisión administrativa firme o sentencia judicial con calidad de cosa juzgada",
            ficha.DeclaracionJurada.vSinVinculoLaboralEstado),
        ("No tener antecedentes penales, judiciales y policiales",
            ficha.DeclaracionJurada.vSinAntecedentesPenales),
        ("No tener deuda por concepto de reparaciones civiles a favor de personas y del Estado establecidas en sentencias con calidad de cosa juzgada, que ameriten la inscripción del suscrito en el Registro de Reparaciones Civiles – REDERECI, creado por Ley N° 30353",
            ficha.DeclaracionJurada.vSinProcesosJudiciales),
        ("No estar inscrito en el Registro Único de Condenados Inhabilitados por Delitos contra la Administración Pública, creado por Decreto Legislativo N° 1243",
            ficha.DeclaracionJurada.vAceptaBasesConcurso),
        ("Gozar de buen estado de salud física y mental",
            "X")
                            };

                            int i = 1;
                            foreach (var req in requisitos)
                            {
                                t.Cell().Border(0.5f).AlignCenter().Text(i.ToString()).FontSize(9);
                                t.Cell().Border(0.5f).Padding(3).Text(req.Texto).FontSize(9).LineHeight(1.2f);
                                t.Cell().Border(0.5f).AlignCenter().Text(req.Valor == "X" ? "X" : "").FontSize(9);
                                i++;
                            }
                        });


                        col.Item().PaddingTop(20).AlignRight().Text("Firma: ______________________").FontSize(10);
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generado automáticamente - {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            })
            .GeneratePdf(stream);

            return stream.ToArray();
        }

        string CalcularAnios(DateTime? inicio, DateTime? fin)
        {
            if (inicio == null || fin == null) return "";
            var totalMeses = ((fin.Value.Year - inicio.Value.Year) * 12) + fin.Value.Month - inicio.Value.Month;
            var anios = totalMeses / 12;
            var meses = totalMeses % 12;
            return $"{anios}a {meses}m";
        }
    }
    }
