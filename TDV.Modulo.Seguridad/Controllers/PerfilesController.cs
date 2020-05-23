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
    public class PerfilesController : ControllerBase
    {

        private readonly PerfilRepository _repository;

        public PerfilesController(PerfilRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }



        [HttpGet("[action]")]
        public async Task<ActionResult> Get()
        {
            var response = await _repository.GetAll();

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
        public async Task<IActionResult> Post([FromBody] Perfil value)
        {
            try
            {
                int id = await _repository.Insert(value);
                value.IdPerfil = id;
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }

            return Ok(value);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] Perfil value)
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

        [HttpDelete("[action]/{idPerfil}/{regUpdateIdUsuario}")]
        public async Task<IActionResult> Deshabilitar(int idPerfil, int regUpdateIdUsuario)
        {
            try
            {
                await _repository.Deshabilitar(idPerfil, regUpdateIdUsuario);
            }
            catch (Exception ex)
            {                
             return BadRequest(ex.Message.ToString());
            }

            return Ok();
        }


    }
}