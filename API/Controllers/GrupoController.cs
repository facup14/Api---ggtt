﻿using DATA.DTOS.Updates;
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
    [Route("grupo")]
    public class GrupoController : ControllerBase
    {
        private readonly ILogger<GrupoController> _logger;
        private readonly IGruposQueryService _gruposQueryService;
        public GrupoController(ILogger<GrupoController> logger, IGruposQueryService productQueryService)
        {
            _logger = logger;
            _gruposQueryService = productQueryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = null, bool order = false)
        {
            try
            {
                IEnumerable<long> gruupos = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    gruupos = ids.Split(',').Select(x => Convert.ToInt64(x));
                }

                var listGrupos = await _gruposQueryService.GetAllAsync(page, take, gruupos, order);

                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = listGrupos
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
                var grupo = await _gruposQueryService.GetAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = grupo,
                };
                return Ok(result);
            }
            catch (EmptyCollectionException ex)
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
        public async Task<IActionResult> Put(UpdateGruposDTO titulo, long id)
        {
            try
            {
                var newGrupo = await _gruposQueryService.PutAsync(titulo, id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = newGrupo
                };
                return Ok(result);
            }
            catch (EmptyCollectionException ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
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
                var deleteList = await _gruposQueryService.DeleteAsync(id);
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
<<<<<<< HEAD
        [HttpPost]
        public async Task<IActionResult> Create(UpdateGruposDTO command)
        {
            try
            {
                var newGrupo = await _gruposQueryService.CreateAsync(command);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = newGrupo
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
=======
       
>>>>>>> REQ-24233
    }
}
