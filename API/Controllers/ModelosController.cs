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
    [Route("modelos")]
    public class ModelosController : ControllerBase
    {

        private readonly ILogger<ModelosController> _logger;
        private readonly IModelosQueryService _modelosQueryService;
        private readonly IMediator _mediator;
        public ModelosController(ILogger<ModelosController> logger, IModelosQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _modelosQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<ModelosDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<long> modelos = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    modelos = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                return await _modelosQueryService.GetAllAsync(page, take, modelos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener los modelos");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<ModelosDTO> Get(long id)
        {
            try
            {
                return await _modelosQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener los modelos, el modelo con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateModeloDTO> Put(UpdateModeloDTO modelo, long id)
        {
            try
            {
                return await _modelosQueryService.PutAsync(modelo, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar el modelo, el modelo con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateModeloCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear el modelo");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ModelosDTO> Delete(long id)
        {
            try
            {
                return await _modelosQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar el modelo, el modelo con id" + " " + id + " " + "no existe");
            }

        }
    }
}