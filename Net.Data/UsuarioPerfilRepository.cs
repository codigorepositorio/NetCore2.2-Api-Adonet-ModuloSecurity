
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
    public class UsuarioPerfilRepository
    {
        private readonly string _cnx;

        public UsuarioPerfilRepository(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("Conexion");
        }


        public async Task<int> Insert(UsuarioPerfil value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_UsuarioPerfil_Insertar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdUsuarioPerfil", Value = value.IdUsuarioPerfil, Direction = System.Data.ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@IdUsuario", value.IdUsuario));
                    cmd.Parameters.Add(new SqlParameter("@IdPerfil", value.IdPerfil));
                    cmd.Parameters.Add(new SqlParameter("@IsUsuarioPerfilActivo", value.IsUsuarioPerfilActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegCreateIdUsuario", value.RegCreateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    int id = (int)cmd.Parameters["@IdUsuarioPerfil"].Value;
                    return id;
                }
            }
        }
        public async Task Update(UsuarioPerfil value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_UsuarioPerfil_Modificar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    cmd.Parameters.Add(new SqlParameter("@IdUsuarioPerfil", value.IdUsuarioPerfil));
                    cmd.Parameters.Add(new SqlParameter("@IdUsuario", value.IdUsuario));
                    cmd.Parameters.Add(new SqlParameter("@IdPerfil", value.IdPerfil));                                        
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task Deshabilitar(int IdUsuarioPerfil, int regUpdateIdUsuario)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_UsuarioPerfil_Deshabilitar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdUsuarioPerfil", IdUsuarioPerfil));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", regUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task<IEnumerable<UsuarioPerfil>> GetByIdUsuarioPerfil(int IdUsuarioPerfil, int IdUsuario, int IdPerfil)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
               
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@IdUsuarioPerfil", IdUsuarioPerfil, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IdUsuario", IdUsuario, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IdPerfil", IdPerfil, DbType.Int32, ParameterDirection.Input);

                var response = await conn.QueryAsync<UsuarioPerfil>("Seg_UsuarioPerfil_Listar", parameter, commandType: CommandType.StoredProcedure);
                return response;
            }

        }


    }
}
