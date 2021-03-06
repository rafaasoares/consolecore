﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository.Interface;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository Repository;

        public IMapper Mapper { get; }

        public EventoController(IProAgilRepository proAgilRepository, IMapper mapper)
        {
            Repository = proAgilRepository;
            Mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var retorno = await Repository.GetAllEventoAsync(true);
                var retornoMapper = Mapper.Map<IEnumerable<EventoDto>>(retorno);
                return Ok(retornoMapper);
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
                var retornoMapper = Mapper.Map<EventoDto>(retorno);
                return Ok(retornoMapper);
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
                var retornoMapper = Mapper.Map<IEnumerable<EventoDto>>(retorno);
                return Ok(retornoMapper);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
        }

        // POST api/value
        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = Mapper.Map<Evento>(model);
                Repository.Add(evento);
                if (await Repository.SaveChangesAsync())
                    return Created($"/api/evento/{evento.Id}", evento);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
            return BadRequest();
        }

        // PUT api/value
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EventoDto model)
        {
            try
            {
                var evento = await Repository.GetEventoByIdAsync(id, false);
                if (evento == null)
                    return NotFound();

                var modelMapper = Mapper.Map(model, evento);

                Repository.Update(modelMapper);
                if (await Repository.SaveChangesAsync())
                    return Created($"/api/evento/{modelMapper.Id}", modelMapper);
            }
            catch (System.Exception ex)
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
                var evento = await Repository.GetEventoByIdAsync(id, false);
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                else
                {
                    return BadRequest("Erro ao tentar realizar upload");
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }

            //return BadRequest("Erro ao tentar realizar upload");
        }
    }
}