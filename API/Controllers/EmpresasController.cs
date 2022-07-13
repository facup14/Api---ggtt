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
    [Route("empresas")]
    public class EmpresasController : ControllerBase
    {

        private readonly ILogger<EmpresasController> _logger;
        private readonly IEmpresasQueryService _empresasQueryService;
        private readonly IMediator _mediator;
        public EmpresasController(ILogger<EmpresasController> logger, IEmpresasQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _empresasQueryService = productQueryService;
            _mediator = mediator;
        }
        //products Trae todas las agurpaciónes
        [HttpGet]
        public async Task<DataCollection<EmpresasDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<int> empresas = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    empresas = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                return await _empresasQueryService.GetAllAsync(page, take, empresas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener las empresas");
            }
        }
        //products/1 Trae la agurpación con el id colocado
        [HttpGet("{id}")]
        public async Task<EmpresasDTO> Get(int id)
        {
            try
            {
                return await _empresasQueryService.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al obtener eempresa, el empresa con id" + " " + id + " " + "no existe");

            }
        }
        //products/id Actualiza una agurpación por el id
        [HttpPut("{id}")]
        public async Task<UpdateEmpresaDTO> Put(UpdateEmpresaDTO empresa, int id)
        {
            try
            {
                return await _empresasQueryService.PutAsync(empresa, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al actualizar el empresa, el empresa con id" + " " + id + " " + "no existe");
            }

        }

        //products Crea una nueva Unidad pasandole solo los parametros NO-NULL
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmpresaCommand command)
        {
            try
            {
                await _mediator.Publish(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al crear la empresa");
            }
        }
        [HttpDelete("{id}")]
        public async Task<EmpresasDTO> Delete(int id)
        {
            try
            {
                return await _empresasQueryService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error al eliminar la empresa, la empresa  con id" + " " + id + " " + "no existe");
            }

        }
    }
}