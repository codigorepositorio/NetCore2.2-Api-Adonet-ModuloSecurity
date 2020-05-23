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
    public class FuncionsController : ControllerBase
    {
        private readonly FuncionRepository _repository;

        public FuncionsController(FuncionRepository repository)
        {
            this._repository = repository ?? throw new ArgumentException(nameof(repository));

        }

        [HttpGet("{idModulo?}/{IdFuncion?}", Name = "ObtenerFuncion")]
        public async Task<ActionResult> Get(int idModulo, int IdFuncion)
        {
            var response = await _repository.GetByIdFuncion(idModulo, IdFuncion);

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
        public async Task<IActionResult> Post([FromBody] Funcion value)
        {
            try
            {
                int id = await _repository.Insert(value);
                value.IdFuncion = id;
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }
            return Ok(value);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] Funcion value)
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

        [HttpDelete("[action]/{idFuncion}/{regUpdateIdUsuario}")]
        public async Task<IActionResult> Deshabilitar(int idFuncion, int regUpdateIdUsuario)
        {
            try
            {
                await _repository.Deshabilitar(idFuncion, regUpdateIdUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            return Ok();
        }

    }
}