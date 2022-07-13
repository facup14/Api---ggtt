using Common.Collection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Queries;
using Service.Queries.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.EventHandlers.Command;
using DATA.DTOS.Updates;
using DATA.DTOS;

namespace API.Controllers
{
    [ApiController]
    [Route("agrupacionessindicales")]
    public class AgrupacionesSindicalesController : ControllerBase
    {

        private readonly ILogger<AgrupacionesSindicalesController> _logger;
        private readonly IAgrupacionesSindicalesQueryService _agrupacionesQueryService;
        private readonly IMediator _mediator;
        public AgrupacionesSindicalesController(ILogger<AgrupacionesSindicalesController> logger, IAgrupacionesSindicalesQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _agrupacionesQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<AgrupacionesSindicalesDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<int> agrupaciones = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    agrupaciones = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                return await _agrupacionesQueryService.GetAllAsync(page, take, agrupaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener las agrupaciones sindicales");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<AgrupacionesSindicalesDTO> Get(int id)
        {
            try
            {
                return await _agrupacionesQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener la unidad, la agrupacion sindical con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateAgrupacionSindicalDTO> Put(UpdateAgrupacionSindicalDTO agrupacion, int id)
        {
            try
            {
                return await _agrupacionesQueryService.PutAsync(agrupacion, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar la unidad, la agrupacion sindical con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateAgrupacionSindicalCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear la agrupacion sindical");
            }
        }
        [HttpDelete("{id}")]
        public async Task<AgrupacionesSindicalesDTO> Delete(int id)
        {
            try
            {
                return await _agrupacionesQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar la unidad, la agrupacion sindical con id" + " " + id + " " + "no existe");
            }

        }
    }
}