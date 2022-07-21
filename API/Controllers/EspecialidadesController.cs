﻿using Common.Collection;
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
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.DTOS;
using System.Net;
using Service.EventHandlers.Command.CreateCommands;

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
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = null)
        {
            try
            {
                IEnumerable<int> especialidades = null;
                if (!string.IsNullOrEmpty(ids))
                {
                    especialidades = ids.Split(',').Select(x => Convert.ToInt32(x));
                }

                var listEspecialidades =  await _especialidadesQueryService.GetAllAsync(page, take, especialidades);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = listEspecialidades
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
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var especialidad =  await _especialidadesQueryService.GetAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = especialidad
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateEspecialidadesDTO especialidad, int id)
        {
            try
            {
                var updateEspecialidad = await _especialidadesQueryService.PutAsync(especialidad, id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = updateEspecialidad
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
        [HttpPost]
        public async Task<IActionResult> Create(UpdateEspecialidadesDTO command)
        {
            try
            {
                var newEspecialidad = await _especialidadesQueryService.CreateAsync(command);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = newEspecialidad
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteEspecialidad = await _especialidadesQueryService.DeleteAsync(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "success",
                    Result = deleteEspecialidad
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
    }
}