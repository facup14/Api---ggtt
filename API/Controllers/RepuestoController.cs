using DATA.DTOS.Updates;
using DATA.Errors;
using DATA.Extensions;
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
    [Route("repuesto")]
    public class RepuestoController : ControllerBase
    {
        private readonly ILogger<RepuestoController> _logger;
        private readonly IRepuestosQueryService _repuestosQueryService;
        public RepuestoController(ILogger<RepuestoController> logger, IRepuestosQueryService productQueryService)
        {
            _logger = logger;
            _repuestosQueryService = productQueryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = null, bool order = false)
        {
            try
            {
                IEnumerable<long> repuestos = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    repuestos = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                var listRepuestos = await _repuestosQueryService.GetAllAsync(page, take, repuestos, order);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = listRepuestos
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var repuesto = await _repuestosQueryService.GetAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = repuesto,
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateRepuestosDTO unidad, long id)
        {
            try
            {
                var newRepuesto = await _repuestosQueryService.PutAsync(unidad, id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = newRepuesto
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
        [HttpPost]
        public async Task<IActionResult> Create(UpdateRepuestosDTO command)
        {
            try
            {
                var newRepuesto = await _repuestosQueryService.CreateAsync(command);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = newRepuesto
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
                var deleteList = await _repuestosQueryService.DeleteAsync(id);
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
