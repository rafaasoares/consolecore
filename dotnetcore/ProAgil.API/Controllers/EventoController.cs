using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository.Interface;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository Repository;

        public EventoController(IProAgilRepository proAgilRepository)
        {
            Repository = proAgilRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var retorno = await Repository.GetAllEventoAsync(true);
                return Ok(retorno);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
        }

        // GET api/values
        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var retorno = await Repository.GetEventoByIdAsync(eventoId, true);
                return Ok(retorno);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
        }

        // GET api/getByTema/tema
        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var retorno = await Repository.GetAllEventoByTemaAsync(tema, true);
                return Ok(retorno);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
        }

        // POST api/value
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                Repository.Add(model);
                if (await Repository.SaveChangesAsync())
                    return Created($"/api/evento/{model.Id}", model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
            return BadRequest();
        }

        // PUT api/value
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Evento model)
        {
            try
            {
                var evento = Repository.GetEventoByIdAsync(id, false);
                if (evento == null)
                    return NotFound();

                Repository.Update(model);
                if (await Repository.SaveChangesAsync())
                    return Created($"/api/evento/{model.Id}", model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
            return BadRequest();
        }

        // DELETE api/value
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = Repository.GetEventoByIdAsync(id, false);
                if (evento == null)
                    return NotFound();

                Repository.Delete(evento);
                if (await Repository.SaveChangesAsync())
                    return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
            return BadRequest();
        }
    }
}