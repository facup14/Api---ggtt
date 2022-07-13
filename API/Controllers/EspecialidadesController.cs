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
    [Route("especialidades")]
    public class EspecialidadesController : ControllerBase
    {

        private readonly ILogger<EspecialidadesController> _logger;
        private readonly IEspecialidadesQueryService _especialidadesQueryService;
        private readonly IMediator _mediator;
        public EspecialidadesController(ILogger<EspecialidadesController> logger, IEspecialidadesQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _especialidadesQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<EspecialidadesDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<int> especialidades = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    especialidades = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                return await _especialidadesQueryService.GetAllAsync(page, take, especialidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener las especialidades");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<EspecialidadesDTO> Get(int id)
        {
            try
            {
                return await _especialidadesQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener especialidades, la especialidad con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateEspecialidadesDTO> Put(UpdateEspecialidadesDTO especialidad, int id)
        {
            try
            {
                return await _especialidadesQueryService.PutAsync(especialidad, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar la especialidad, la especialidad con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateEspecialidadCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear la especialidad");
            }
        }
        [HttpDelete("{id}")]
        public async Task<EspecialidadesDTO> Delete(int id)
        {
            try
            {
                return await _especialidadesQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar la especialidad, la especialidad  con id" + " " + id + " " + "no existe");
            }

        }
    }
}