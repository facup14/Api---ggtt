using Common.Collection;
using MediatR;
using DATA.DTOS.Updates;
using DATA.Errors;
using DATA.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Queries;
using Service.Queries.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.EventHandlers.Command;
using DATA.DTOS;

namespace API.Controllers
{
    [ApiController]
    [Route("marcas")]
    public class MarcasController : ControllerBase
    {

        private readonly ILogger<MarcasController> _logger;
        private readonly IMarcasQueryService _marcasQueryService;
        private readonly IMediator _mediator;
        public MarcasController(ILogger<MarcasController> logger, IMarcasQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _marcasQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<MarcasDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<long> marcas = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    marcas = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                return await _marcasQueryService.GetAllAsync(page, take, marcas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener las marcas");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<MarcasDTO> Get(long id)
        {
            try
            {
                return await _marcasQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener la marca, la marca con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateMarcaDTO> Put(UpdateMarcaDTO marca, long id)
        {
            try
            {
                return await _marcasQueryService.PutAsync(marca, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar la marca, la marca con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateMarcaCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear la marca");
            }
        }
        [HttpDelete("{id}")]
        public async Task<MarcasDTO> Delete(long id)
        {
            try
            {
                return await _marcasQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar la marca, la marca con id" + " " + id + " " + "no existe");
            }

        }
    }
}