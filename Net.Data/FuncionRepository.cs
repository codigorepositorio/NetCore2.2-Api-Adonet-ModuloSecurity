using Dapper;
using Microsoft.Extensions.Configuration;
using Net.Business.Entities;
using Net.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Net.Data
{
    public class FuncionRepository
    {
        private readonly string _cnx;

        public FuncionRepository(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("Conexion");
        }
        public async Task<IEnumerable<dtoFuncion>> GetByIdFuncion(int IdModulo, int IdFuncion)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@IdModulo", IdModulo, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IdFuncion", IdFuncion, DbType.Int32, ParameterDirection.Input);
                
                var response = await conn.QueryAsync<dtoFuncion>("Seg_Funcion_Listar", parameter, commandType: CommandType.StoredProcedure);

                return response;
            }
        }
        public async Task<int> Insert(Funcion value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Funcion_Insertar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdFuncion", Value = value.IdFuncion, Direction = System.Data.ParameterDirection.Output });
                    
                    cmd.Parameters.Add(new SqlParameter("@IdModulo", value.IdModulo));
                    cmd.Parameters.Add(new SqlParameter("@NomFuncion", value.NomFuncion));
                    cmd.Parameters.Add(new SqlParameter("@CodAcceso", value.CodAcceso));
                    cmd.Parameters.Add(new SqlParameter("@IsFuncionActivo", value.IsFuncionActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegCreateIdUsuario", value.RegCreateIdUsuario));
                    
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    int id = (int)cmd.Parameters["@IdFuncion"].Value;
                    return id;
                }
            }
        }
        public async Task Update(Funcion value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Funcion_Modificar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdFuncion", value.IdFuncion));
                    cmd.Parameters.Add(new SqlParameter("@IdModulo", value.IdModulo));
                    cmd.Parameters.Add(new SqlParameter("@NomFuncion", value.NomFuncion));
                    cmd.Parameters.Add(new SqlParameter("@CodAcceso", value.CodAcceso));
                    cmd.Parameters.Add(new SqlParameter("@IsFuncionActivo", value.IsFuncionActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Deshabilitar(int idFuncion, int regUpdateIdUsuario)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Funcion_Deshabilitar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdFuncion", idFuncion));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", regUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


    }
}
