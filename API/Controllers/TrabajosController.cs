using DATA.DTOS.Updates;
using DATA.Errors;
using DATA.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("Trabajos")]
    public class TrabajosController : ControllerBase
    {
        private readonly ILogger<TrabajosController> _logger;
        private readonly ITrabajosQueryService _trabajosQueryService;
        private readonly IMediator _mediator;
        public TrabajosController(ILogger<TrabajosController> logger, ITrabajosQueryService productQueryService, IMediator mediator)
        {
            _logger = logger;
            _trabajosQueryService = productQueryService;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<long> trabajos = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    trabajos = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                var listTrabajos = await _trabajosQueryService.GetAllAsync(page, take, trabajos);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = listTrabajos
                };
                return Ok(result);

            }
            catch (EmptyCollectionException ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.MultiStatus,
                    Message = ex.Message,
                    Result = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Server error",
                    Result = null
                });
            }
        }
        //products/1 Trae la unidad con el id colocado
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var trabajo = await _trabajosQueryService.GetAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = trabajo,
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ex.Message,
                    Result = null
                });

            }
        }
        //products/id Actualiza una Unidad por el id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateTrabajoDTO unidad, long id)
        {
            try
            {
                var newTrabajo = await _trabajosQueryService.PutAsync(unidad, id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = newTrabajo
                };
                return Ok(result);
            }
            catch (EmptyCollectionException ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Server error",
                    Result = null
                });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var deleteList = await _trabajosQueryService.DeleteAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = deleteList
                };
                return Ok(result);
            }
            catch (EmptyCollectionException ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.MultiStatus,
                    Message = "Server error",
                    Result = null
                });
            }

        }
    }
}
