using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Data;
using Net.Business.Entities;
namespace TDV.Modulo.Seguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosController : ControllerBase
    {

        private readonly ModuloRepository _repository;

        public ModulosController(ModuloRepository repository)
        {
            this._repository = repository ?? throw new ArgumentException(nameof(repository));

        }


        //[HttpGet("[action]")]
        //public async Task<ActionResult> Get()
        //{
        //    var response = await _repository.GetAll();

        //    try
        //    {
        //        if (response == null) { return BadRequest("No se encontraron datos"); }
        //    }
        //    catch (Exception ex)
        //    {
        //        BadRequest(ex.ToString());
        //    }

        //    return Ok(response);
        //}


        [HttpGet("{idAplicativo}/{idModulo?}")]
        public async Task<ActionResult> Get(int idAplicativo, int idModulo)
        {
            var response = await _repository.GetByIdModulo(idAplicativo,idModulo);

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
        public async Task<IActionResult> Post([FromBody] Modulos value)
        {
            try
            {
                int id = await _repository.Insert(value);
                value.IdModulo = id;
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex.Message.ToString());
            }

            return Ok(value);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] Modulos value)
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

        [HttpDelete("[action]/{idModulo}/{regUpdateIdUsuario}")]
        public async Task<IActionResult> Deshabilitar(int idModulo, int regUpdateIdUsuario)
        {
            try
            {
                await _repository.Deshabilitar(idModulo, regUpdateIdUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            return Ok();
        }

    }
}