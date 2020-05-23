using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Net.DTO;
using Net.Business.Entities;
using Dapper;
using System.Linq;

namespace Net.Data
{
    public class UsuarioRepository
    {
        private readonly string _cnx;

        public UsuarioRepository(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("Conexion");
        }
        public async Task<int> Insert(Usuario value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Usuario_Insertar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdUsuario", Value = value.IdUsuario, Direction = System.Data.ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@Login", value.Login));
                    cmd.Parameters.Add(new SqlParameter("@Password", value.Password));
                    cmd.Parameters.Add(new SqlParameter("@Nombres", value.Nombres));
                    cmd.Parameters.Add(new SqlParameter("@ApPaterno", value.ApellidoPaterno));
                    cmd.Parameters.Add(new SqlParameter("@ApMaterno", value.ApellidoMaterno));
                    cmd.Parameters.Add(new SqlParameter("@Email", value.CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@IsUsuarioActivo", value.IsUsuarioActivo));
                    cmd.Parameters.Add(new SqlParameter("@IsCambioPassword", value.IsCambioPassword));
                    cmd.Parameters.Add(new SqlParameter("@RegCreateIdUsuario", value.RegCreateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    int id = (int)cmd.Parameters["@IdUsuario"].Value;
                    return id;
                }
            }
        }
        public async Task Update(Usuario value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Usuario_modificar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdUsuario", value.IdUsuario));
                    cmd.Parameters.Add(new SqlParameter("@Login", value.Login));
                    cmd.Parameters.Add(new SqlParameter("@Password", value.Password));
                    cmd.Parameters.Add(new SqlParameter("@Nomnbres", value.Nombres));
                    cmd.Parameters.Add(new SqlParameter("@ApPaterno", value.ApellidoPaterno));
                    cmd.Parameters.Add(new SqlParameter("@ApMaterno", value.ApellidoMaterno));
                    cmd.Parameters.Add(new SqlParameter("@Email", value.CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@IsUsuarioActivo", value.IsUsuarioActivo));
                    cmd.Parameters.Add(new SqlParameter("@IsCambioPassword", value.IsCambioPassword));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Deshabilitar(Usuario value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Seg_Usuario_Deshabilitar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdUsuario", value.IdUsuario));
                    cmd.Parameters.Add(new SqlParameter("@RegUpdateIdUsuario", value.RegUpdateIdUsuario));
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task<IEnumerable<dtoUsuario>> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                var parametros = new DynamicParameters();
                var response = await conn.QueryAsync<dtoUsuario>("Seg_Usuario_Listar", parametros);
                return response;
            }
        }





        public async Task demo(Demo value)
        {
            using (SqlConnection conn = new SqlConnection(_cnx))
            {
                using (SqlCommand cmd = new SqlCommand("spdemo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                   
                    cmd.Parameters.Add(new SqlParameter("@COL1", value.COL1));
                    cmd.Parameters.Add(new SqlParameter("@COL2", value.COL2));
                    cmd.Parameters.Add(new SqlParameter("@COL3", value.COL3));
                    cmd.Parameters.Add(new SqlParameter("@COL4", value.COL4));
                    cmd.Parameters.Add(new SqlParameter("@COL5", value.COL5));                    
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

    }
}

