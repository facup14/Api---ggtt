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
    [Route("provincias")]
    public class ProvinciasController : ControllerBase
    {

        private readonly ILogger<ProvinciasController> _logger;
        private readonly IProvinciasQueryService _provinciasQueryService;
        private readonly IMediator _mediator;
        public ProvinciasController(ILogger<ProvinciasController> logger, IProvinciasQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _provinciasQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<ProvinciasDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<long> provincias = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    provincias = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                return await _provinciasQueryService.GetAllAsync(page, take, provincias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener las provincias");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<ProvinciasDTO> Get(long id)
        {
            try
            {
                return await _provinciasQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener la provincia, la provincia con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateProvinciaDTO> Put(UpdateProvinciaDTO provincia, long id)
        {
            try
            {
                return await _provinciasQueryService.PutAsync(provincia, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar la provincia, la provincia con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateProvinciaCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear la provincia");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ProvinciasDTO> Delete(long id)
        {
            try
            {
                return await _provinciasQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar la provincia, la provincia con id" + " " + id + " " + "no existe");
            }

        }
    }
}