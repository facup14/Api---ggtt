using DATA.DTOS.Updates;
using DATA.Errors;
using DATA.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("agrupacionessindicales")]
    public class AgrupacionesSindicalesController : ControllerBase
    {

        private readonly ILogger<AgrupacionesSindicalesController> _logger;
        private readonly IAgrupacionesSindicalesQueryService _agrupacionesQueryService;
        public AgrupacionesSindicalesController(ILogger<AgrupacionesSindicalesController> logger, IAgrupacionesSindicalesQueryService productQueryService)
        {
            _logger = logger;
            _agrupacionesQueryService = productQueryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = null, bool order = false)
        {
            try
            {
                IEnumerable<int> agrupaciones = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    agrupaciones = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                var listAgrupaciones = await _agrupacionesQueryService.GetAllAsync(page, take, agrupaciones, order);
                
                var result = new GetResponse()
                {
                   StatusCode = (int)HttpStatusCode.OK,
                   Message = "success",
                   Result = listAgrupaciones
                };
                
                return Ok(result);
            }
            catch(EmptyCollectionException ex)
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
                var agrupacion = await _agrupacionesQueryService.GetAsync(id);
                
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = agrupacion
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
        public async Task<IActionResult> Put(UpdateAgrupacionSindicalDTO agrupacion, int id)
        {
            try
            {
                var updateAgrupacion = await _agrupacionesQueryService.PutAsync(agrupacion, id);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = updateAgrupacion
                };
                return Ok(result);
            }
            catch(EmptyCollectionException ex)
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
        public async Task<IActionResult> Create(UpdateAgrupacionSindicalDTO command)
        {
            try
            {
                var createAgrupacion = await _agrupacionesQueryService.CreateAsync(command);

               
                return Ok(createAgrupacion);
            }
            catch(EmptyCollectionException ex)
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
                var deleteAgrupacion = await _agrupacionesQueryService.DeleteAsync(id);
                
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = deleteAgrupacion
                };
                return Ok(result);
            }
            catch(EmptyCollectionException ex)
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