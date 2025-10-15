using System.Data;
using System.Data.SqlClient;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Repositories
{
    public class ExperienciaLaboralRepository : IExperienciaLaboralRepository
    {
        private readonly string _connectionString;

        public ExperienciaLaboralRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public int Insertar(ExperienciaLaboralDto dto)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_InsertarExperienciaLaboral", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
            cmd.Parameters.AddWithValue("@vEntidad", dto.vEntidad);
            cmd.Parameters.AddWithValue("@vUnidadOrganica", (object?)dto.vUnidadOrganica ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vCargo", dto.vCargo);
            cmd.Parameters.AddWithValue("@cSector", (object?)dto.cSector ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cTipoExperienciaLaboral", (object?)dto.cTipoExperienciaLaboral ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@bActAgricolas", dto.bActAgricolas);
            cmd.Parameters.AddWithValue("@bActAgropecuarias", dto.bActAgropecuarias);
            cmd.Parameters.AddWithValue("@bTemasSanitarios", dto.bTemasSanitarios);
            cmd.Parameters.AddWithValue("@bAccesoMercadosExternos", dto.bAccesoMercadosExternos);
            cmd.Parameters.AddWithValue("@dFechaInicio", dto.dFechaInicio);
            cmd.Parameters.AddWithValue("@dFechaFin", (object?)dto.dFechaFin ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vFunciones", (object?)dto.vFunciones ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@iCodUsuarioRegistra", dto.iCodUsuarioRegistra);

            conn.Open();
            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public void Actualizar(ExperienciaLaboralDto dto)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_ActualizarExperienciaLaboral", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@iCodExperienciaLaboral", dto.iCodExperienciaLaboral);
            cmd.Parameters.AddWithValue("@iCodUsuario", dto.iCodUsuario);
            cmd.Parameters.AddWithValue("@vEntidad", dto.vEntidad);
            cmd.Parameters.AddWithValue("@vUnidadOrganica", (object?)dto.vUnidadOrganica ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vCargo", dto.vCargo);
            cmd.Parameters.AddWithValue("@cSector", (object?)dto.cSector ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cTipoExperienciaLaboral", (object?)dto.cTipoExperienciaLaboral ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@bActAgricolas", dto.bActAgricolas);
            cmd.Parameters.AddWithValue("@bActAgropecuarias", dto.bActAgropecuarias);
            cmd.Parameters.AddWithValue("@bTemasSanitarios", dto.bTemasSanitarios);
            cmd.Parameters.AddWithValue("@bAccesoMercadosExternos", dto.bAccesoMercadosExternos);
            cmd.Parameters.AddWithValue("@dFechaInicio", dto.dFechaInicio);
            cmd.Parameters.AddWithValue("@dFechaFin", (object?)dto.dFechaFin ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vFunciones", (object?)dto.vFunciones ?? DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_EliminarExperienciaLaboral", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@iCodExperienciaLaboral", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<ExperienciaLaboralDto> Listar()
        {
            var lista = new List<ExperienciaLaboralDto>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_ListarExperienciaLaboral", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(Mapear(dr));
            }

            return lista;
        }

        public List<ExperienciaLaboralDto> ListarPorUsuario(int iCodUsuario)
        {
            var lista = new List<ExperienciaLaboralDto>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PA_ListarExperienciaLaboralPorUsuario", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@iCodUsuario", iCodUsuario);
            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(Mapear(dr));
            }

            return lista;
        }

        // 🧠 Método reutilizable para mapear datos
        private ExperienciaLaboralDto Mapear(SqlDataReader dr)
        {
            return new ExperienciaLaboralDto
            {
                iCodExperienciaLaboral = Convert.ToInt32(dr["iCodExperienciaLaboral"]),
                iCodUsuario = Convert.ToInt32(dr["iCodUsuario"]),
                vEntidad = dr["vEntidad"].ToString()!,
                vUnidadOrganica = dr["vUnidadOrganica"].ToString(),
                vCargo = dr["vCargo"].ToString()!,
                cSector = dr["cSector"].ToString(),
                cTipoExperienciaLaboral = dr["cTipoExperienciaLaboral"].ToString(),
                bActAgricolas = Convert.ToBoolean(dr["bActAgricolas"]),
                bActAgropecuarias = Convert.ToBoolean(dr["bActAgropecuarias"]),
                bTemasSanitarios = Convert.ToBoolean(dr["bTemasSanitarios"]),
                bAccesoMercadosExternos = Convert.ToBoolean(dr["bAccesoMercadosExternos"]),
                dFechaInicio = Convert.ToDateTime(dr["dFechaInicio"]),
                dFechaFin = dr["dFechaFin"] == DBNull.Value ? null : Convert.ToDateTime(dr["dFechaFin"]),
                vFunciones = dr["vFunciones"].ToString(),
                iCodUsuarioRegistra = Convert.ToInt32(dr["iCodUsuarioRegistra"]),
                dtFechaRegistro = Convert.ToDateTime(dr["dtFechaRegistro"]),
                bActivo = Convert.ToBoolean(dr["bActivo"])
            };
        }
    }
}
