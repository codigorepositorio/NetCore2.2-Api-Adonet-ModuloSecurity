using Dapper;
using Microsoft.Extensions.Configuration;
using Net.Business.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Net.Data
{
    public class PerfilRepository
    {
        private readonly string _cnx;

        public PerfilRepository(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("Conexion");
        }
        public async Task<int> Insert(Perfil value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Perfil_Insertar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdPerfil", Value = value.IdPerfil, Direction = System.Data.ParameterDirection.Output });

                    cmd.Parameters.Add(new SqlParameter("@NomPerfil", value.NomPerfil));
                    cmd.Parameters.Add(new SqlParameter("@IsPerfilcActivo", value.IsPerfilActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegCreateIdUsuario", value.RegCreateIdUsuario));

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    int id = (int)cmd.Parameters["@IdPerfil"].Value;
                    return id;
                }
            }
        }
        public async Task Update(Perfil value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Perfil_Modificar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdPerfil", value.IdPerfil));
                    cmd.Parameters.Add(new SqlParameter("@NomPerfil", value.NomPerfil));
                    cmd.Parameters.Add(new SqlParameter("@IsPerfilcActivo", value.IsPerfilActivo));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task<IEnumerable<Perfil>> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {                
                var response = await conn.QueryAsync<Perfil>("Seg_Perfil_Listar");
                return response;
            }
        }
        public async Task Deshabilitar(int idPerfil, int regUpdateIdUsuario)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Perfil_Deshabilitar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdPerfil", idPerfil));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario",regUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
