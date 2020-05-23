using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Business.Entities;
using Net.Data;

namespace TDV.Modulo.Seguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPerfilController : ControllerBase
    {
        private readonly UsuarioPerfilRepository _repository;
        public UsuarioPerfilController(UsuarioPerfilRepository repository)
        {
            this._repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        [HttpGet("{IdUsuarioPerfil?}/{IdUsuario?}/{IdPerfil?}")]
        public async Task<ActionResult> Get(int IdUsuarioPerfil, int IdUsuario,int IdPerfil)
        {
            var response = await _repository.GetByIdUsuarioPerfil(IdUsuarioPerfil, @IdUsuario, IdPerfil);

            try
            {
                if (response == null) { return BadRequest("No se encontraron datos"); }
            }
            catch (Exception ex)
            {
                BadRequest(ex.ToString());
            }

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] UsuarioPerfil value)
        {
            try
            {
                int id = await _repository.Insert(value);
                value.IdUsuarioPerfil = id;
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }
            return Ok(value);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] UsuarioPerfil value)
        {
            try
            {
                await _repository.Update(value);

            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }

            return Ok(value);
        }

        [HttpDelete("[action]/{idUsuarioPerfil}/{regUpdateIdUsuario}")]
        public async Task<IActionResult> Deshabilitar(int idUsuarioPerfil, int regUpdateIdUsuario)
        {
            try
            {
                await _repository.Deshabilitar(idUsuarioPerfil, regUpdateIdUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            return Ok();
        }

    }
}

