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

namespace API.Controllers
{
    [ApiController]
    [Route("unidades")]
    public class UnidadesController : ControllerBase
    {

        private readonly ILogger<UnidadesController> _logger;
        private readonly IUnidadesQueryService _unidadesQueryService;
        private readonly IMediator _mediator;
        public UnidadesController(ILogger<UnidadesController> logger, IUnidadesQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _unidadesQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las Unidades
        [HttpGet]
        public async Task<DataCollection<UnidadesDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<int> unidades = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    unidades = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                return await _unidadesQueryService.GetAllAsync(page, take, unidades); 
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener las unidades");
            }
        }
        //products/1 Trae la unidad con el id colocado
        [HttpGet("{id}")]
        public async Task<UnidadesDTO> Get(int id)
        {
            try
            {
                return await _unidadesQueryService.GetAsync(id);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener la unidad, la unidad con id" + " " + id + " " + "no existe");
                
            }
        }
        //products/id Actualiza una Unidad por el id
        [HttpPut("{id}")]
        public async Task<UpdateUnidadDTO> Put(UpdateUnidadDTO unidad, int id)            
        {
            try
            {
                return await _unidadesQueryService.PutAsync(unidad, id);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar la unidad, la unidad con id" + " " + id + " " + "no existe");
            }
            
        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateUnidadCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();  
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear la unidad");
            }
        }
        [HttpDelete("{id}")]
        public async Task<UnidadesDTO> Delete(int id)
        {
            try
            {
               return await _unidadesQueryService.DeleteAsync(id); 
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar la unidad, la unidad con id" +" "+ id + " "+ "no existe");
            }
            
        }
    }
}
