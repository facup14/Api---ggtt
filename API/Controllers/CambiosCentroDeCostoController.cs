using DATA.Errors;
using DATA.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Queries;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("cambios-centrodecoso")]
    public class CambiosCentroDeCosto : Controller
    {
        private readonly ILogger<CambiosCentroDeCosto> _logger;
        private readonly ICambiosCentroDeCostoQueryService _cCentroDeCostoQueryService;
        public CambiosCentroDeCosto(ILogger<CambiosCentroDeCosto> logger, ICambiosCentroDeCostoQueryService productQueryService)
        {
            _logger = logger;
            _cCentroDeCostoQueryService = productQueryService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var centroCosto = _cCentroDeCostoQueryService.GetHistoricosCambioCentroDeCosto(id);
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
