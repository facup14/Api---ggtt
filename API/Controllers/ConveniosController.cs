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

namespace API.Controllers
{
    [ApiController]
    [Route("convenios")]
    public class ConveniosController : ControllerBase
    {

        private readonly ILogger<ConveniosController> _logger;
        private readonly IConveniosQueryService _conveniosQueryService;
        private readonly IMediator _mediator;
        public ConveniosController(ILogger<ConveniosController> logger, IConveniosQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _conveniosQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<ConveniosDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<int> convenios = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    convenios = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                return await _conveniosQueryService.GetAllAsync(page, take, convenios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener los convenios");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<ConveniosDTO> Get(int id)
        {
            try
            {
                return await _conveniosQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener el convenio, el convenio con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateConvenioDTO> Put(UpdateConvenioDTO convenio, int id)
        {
            try
            {
                return await _conveniosQueryService.PutAsync(convenio, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar el convenio, el convenio con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateConvenioCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear el convenio");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ConveniosDTO> Delete(int id)
        {
            try
            {
                return await _conveniosQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar el convenio, el coenvenio  con id" + " " + id + " " + "no existe");
            }

        }
    }
}