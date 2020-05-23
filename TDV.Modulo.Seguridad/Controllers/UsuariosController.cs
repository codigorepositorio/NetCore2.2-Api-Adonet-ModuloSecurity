using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Business.Entities;
using Net.Data;
using Net.DTO;

namespace TDV.Modulo.Seguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public UsuariosController(UsuarioRepository repository)
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
            catch(Exception ex)
            {
                BadRequest(ex.ToString());
            }

            return Ok(response);
        }


        [HttpGet("[action]")]
        

        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] Usuario value)
        {
            try
            {
                int id = await _repository.Insert(value);
                value.IdUsuario = id;               
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }
            
            return Ok(value);
        }








        [HttpPost("[action]")]
        public async Task<IActionResult> demo([FromBody] Demo value)
        {
            try
            {
                 await _repository.demo(value);
                
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }

            return Ok(value);
        }






        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] Usuario value)
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


        [HttpPut("[action]")]
        public async Task<IActionResult>Deshabilitar([FromBody] Usuario value)
        {
            try
            {
                await _repository.Deshabilitar(value);
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }

            return Ok(value);
        }

    }
}