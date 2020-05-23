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
    public class AplicativoRepository
    {
        private readonly string _cnx;

        public AplicativoRepository(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("Conexion");
        }

        public async Task<int> Insert(Aplicativo value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Aplicativo_Insertar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdAplicativo", Value = value.IdAplicativo, Direction = System.Data.ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@CodAplicativo", value.CodAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@NomAplicativo", value.NomAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@IsAplicativoActivo", value.IsAplicativoActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegCreateIdUsuario", value.RegCreateIdUsuario));


                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    int id = (int) cmd.Parameters["@IdAplicativo"].Value;
                    return id;
                }
            }
        }
        public async Task Update(Aplicativo value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Aplicativo_Modificar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdAplicativo", value.IdAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@CodAplicativo", value.CodAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@NomAplicativo", value.NomAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@IsAplicativoActivo", value.IsAplicativoActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task Deshabilitar(Aplicativo value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Aplicativo_Deshabilitar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdAplicativo", value.IdAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
       public async Task<IEnumerable<dtoAplicativo>> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {                
                var response = await conn.QueryAsync<dtoAplicativo>("Seg_Aplicativo_Listar", commandType: CommandType.StoredProcedure);

               return response;
            }

        }

    }

}


