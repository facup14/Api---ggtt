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
using DATA.DTOS.Updates;

namespace API.Controllers
{
    [ApiController]
    [Route("estadosunidades")]
    public class EstadosUnidadesController : ControllerBase
    {

        private readonly ILogger<EstadosUnidadesController> _logger;
        private readonly IEstadosUnidadesQueryService _estadosunidadesQueryService;
        private readonly IMediator _mediator;
        public EstadosUnidadesController(ILogger<EstadosUnidadesController> logger, IEstadosUnidadesQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _estadosunidadesQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<EstadosUnidadesDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<long> estadosunidades = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    estadosunidades = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                return await _estadosunidadesQueryService.GetAllAsync(page, take, estadosunidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener los estados");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<EstadosUnidadesDTO> Get(long id)
        {
            try
            {
                return await _estadosunidadesQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener estados, el estado con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateEstadoUnidadDTO> Put(UpdateEstadoUnidadDTO estadounidad, long id)
        {
            try
            {
                return await _estadosunidadesQueryService.PutAsync(estadounidad, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar estados, el estado con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateEstadoUnidadCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear el estado");
            }
        }
        [HttpDelete("{id}")]
        public async Task<EstadosUnidadesDTO> Delete(long id)
        {
            try
            {
                return await _estadosunidadesQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar el estado, el estado  con id" + " " + id + " " + "no existe");
            }

        }
    }
}