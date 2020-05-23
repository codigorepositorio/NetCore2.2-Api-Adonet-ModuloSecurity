using Dapper;
using Microsoft.Extensions.Configuration;
using Net.Business.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ModuloRepository
    {
        private readonly string _cnx;

        public ModuloRepository(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("Conexion");
        }



        public async Task<int> Insert(Modulos value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Modulo_Insertar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdModulo", Value = value.IdModulo, Direction = System.Data.ParameterDirection.Output });

                    cmd.Parameters.Add(new SqlParameter("@IdAplicativo", value.IdAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@NomModulo", value.NomModulo));
                    cmd.Parameters.Add(new SqlParameter("@IsModuloActivo", value.IsModuloActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegCreateIdUsuario", value.RegCreateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    int id = (int)cmd.Parameters["@IdModulo"].Value;

                    return id;
                }
            }
        }

        public async Task Update(Modulos value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Modulo_Modificar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdModulo", value.IdModulo));
                    cmd.Parameters.Add(new SqlParameter("@IdAplicativo", value.IdAplicativo));
                    cmd.Parameters.Add(new SqlParameter("@NomModulo", value.NomModulo));
                    cmd.Parameters.Add(new SqlParameter("@IsModuloActivo", value.IsModuloActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task<IEnumerable<Modulos>> GetByIdModulo(int IdAplicativo, int IdModulo)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@IdAplicativo", IdAplicativo, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IdModulo", IdModulo, DbType.Int32, ParameterDirection.Input);
                var response = await conn.QueryAsync<Modulos>("SEG_Modulo_Listar", parameter, commandType: CommandType.StoredProcedure);              
                return response;
            }

        }

        public async Task Deshabilitar(int idModulo, int regUpdateIdUsuario)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Modulo_Deshabilitar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdModulo", idModulo));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", regUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

    }
}
