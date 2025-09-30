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
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly string _connectionString;

        public TipoDocumentoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<TipoDocumentoDto>> ListarAsync()
        {
            var lista = new List<TipoDocumentoDto>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_TipoDocumento_Listar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new TipoDocumentoDto
                        {
                            CodTipoDocumento = reader.GetInt32(reader.GetOrdinal("iTipoDocumento")),
                            NombreDocumento = reader.GetString(reader.GetOrdinal("vNombreDocumento")),
                            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtFechaRegistro")),
                            Activo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                        });
                    }
                }
            }

            return lista;
        }
    }
}
