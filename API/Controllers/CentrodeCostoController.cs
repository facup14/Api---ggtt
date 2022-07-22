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
    [Route("centrosdecosto")]
    public class CentrodeCostoController : ControllerBase
    {

        private readonly ILogger<CentrodeCostoController> _logger;
        private readonly ICentrodeCostoQueryService _centrosdecostoQueryService;
        public CentrodeCostoController(ILogger<CentrodeCostoController> logger, ICentrodeCostoQueryService productQueryService)
        {
            _logger = logger;
            _centrosdecostoQueryService = productQueryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = null, bool order = false)
        {
            try
            {
                IEnumerable<long> centrosdecosto = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    centrosdecosto = ids.Split(',').Select(x => Convert.ToInt64(x));
                }
                
                var listCentros =  await _centrosdecostoQueryService.GetAllAsync(page, take, centrosdecosto, order);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = listCentros
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
                    Message = ex.Message,
                    Result = null
                });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var centroCosto = await _centrosdecostoQueryService.GetAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = centroCosto
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
                    Message = ex.Message,
                    Result = null
                });

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateCentrodeCostoDTO centrodecosto, int id)
        {
            try
            {
                var centroCosto = await _centrosdecostoQueryService.PutAsync(centrodecosto, id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = centroCosto
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
                    Message = ex.Message,
                    Result = null
                });
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(UpdateCentrodeCostoDTO command)
        {
            try
            {
                var newCentroCosto = await _centrosdecostoQueryService.CreateAsync(command);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = newCentroCosto
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
                    Message = ex.Message,
                    Result = null
                });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var centroCosto = await _centrosdecostoQueryService.DeleteAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = centroCosto
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
                    Message = ex.Message,
                    Result = null
                });
            }

        }
    }
}