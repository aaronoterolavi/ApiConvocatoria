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
    public class EstadoRepository : IEstadoRepository
    {
        private readonly string _connectionString;

        public EstadoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<EstadoDto>> ListarAsync()
        {
            var lista = new List<EstadoDto>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_Estado_Listar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new EstadoDto
                        {
                            CodEstado = reader.GetInt32(reader.GetOrdinal("iCodEstado")),
                            Descripcion = reader.GetString(reader.GetOrdinal("vDescripcion")),
                            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("dtfechaRegistro")),
                            Activo = reader.GetBoolean(reader.GetOrdinal("bActivo"))
                        });
                    }
                }
            }

            return lista;
        }
    }
}
