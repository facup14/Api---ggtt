using DATA.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Queries;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("unidadeschoferes")]
    public class UnidadesChoferesController : ControllerBase
    {
        private readonly ILogger<UnidadesChoferesController> _logger;
        private readonly IUnidadesChoferesQueryService _unidadesChoferesQueryService;
        public UnidadesChoferesController(ILogger<UnidadesChoferesController> logger, IUnidadesChoferesQueryService productQueryService)
        {
            _logger = logger;
            _unidadesChoferesQueryService = productQueryService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var unidad =  _unidadesChoferesQueryService.GetUnidadesChoferes(id);
                var result = new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = unidad,
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
    }
}
