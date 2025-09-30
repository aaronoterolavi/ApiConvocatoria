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
    public class UnidadZonalRepository : IUnidadZonalRepository
    {
        private readonly string _connectionString;

        public UnidadZonalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<UnidadZonalDto>> ListarAsync()
        {
            var lista = new List<UnidadZonalDto>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_UnidadZonal_Listar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new UnidadZonalDto
                        {
                            CodUnidadZonal = reader.GetInt32(reader.GetOrdinal("iCodUnidadZonal")),
                            Nombre = reader.GetString(reader.GetOrdinal("vNombre")),
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
