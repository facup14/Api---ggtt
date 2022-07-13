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
    [Route("centrosdecosto")]
    public class CentrodeCostoController : ControllerBase
    {

        private readonly ILogger<CentrodeCostoController> _logger;
        private readonly ICentrodeCostoQueryService _centrosdecostoQueryService;
        private readonly IMediator _mediator;
        public CentrodeCostoController(ILogger<CentrodeCostoController> logger, ICentrodeCostoQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _centrosdecostoQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas los centro de costo
        [HttpGet]
        public async Task<DataCollection<CentrodeCostoDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<long> centrosdecosto = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    centrosdecosto = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                return await _centrosdecostoQueryService.GetAllAsync(page, take, centrosdecosto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener los centros de costo");
            }
        }
        //products/1 Trae el centro de costo con el id colocado
        [HttpGet("{id}")]
        public async Task<CentrodeCostoDTO> Get(int id)
        {
            try
            {
                return await _centrosdecostoQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener la unidad, el centro de costo con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una centro de costo por el id
        [HttpPut("{id}")]
        public async Task<UpdateCentrodeCostoDTO> Put(UpdateCentrodeCostoDTO centrodecosto, int id)
        {
            try
            {
                return await _centrosdecostoQueryService.PutAsync(centrodecosto, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar la unidad, el centro de costo con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nuevo centro de costo pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateCentrodeCostoCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear el centro de costo");
            }
        }
        [HttpDelete("{id}")]
        public async Task<CentrodeCostoDTO> Delete(int id)
        {
            try
            {
                return await _centrosdecostoQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar la unidad, el centro de costo con id" + " " + id + " " + "no existe");
            }

        }
    }
}