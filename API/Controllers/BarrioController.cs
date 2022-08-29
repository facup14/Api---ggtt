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
    [Route("barrio")]
    public class BarrioController : ControllerBase
    {
        private readonly ILogger<BarrioController> _logger;
        private readonly IBarriosQueryService _barriosQueryService;
        public BarrioController(ILogger<BarrioController> logger, IBarriosQueryService productQueryService)
        {
            _logger = logger;
            _barriosQueryService = productQueryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = null, bool order = false)
        {
            try
            {
                IEnumerable<int> barrios = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    barrios = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                var listBarrios = await _barriosQueryService.GetAllAsync(page, take, barrios, order);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = listBarrios
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var barrio = await _barriosQueryService.GetAsync(id);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = barrio
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateBarrioDTO agrupacion, int id)
        {
            try
            {
                var updateBarrio = await _barriosQueryService.PutAsync(agrupacion, id);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = updateBarrio
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
                    Message = "server error",
                    Result = null
                });
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(UpdateBarrioDTO command)
        {
            try
            {
                var newBarrio = await _barriosQueryService.CreateAsync(command);
                
                return Ok(newBarrio);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteBarrio = await _barriosQueryService.DeleteAsync(id);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = deleteBarrio
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
