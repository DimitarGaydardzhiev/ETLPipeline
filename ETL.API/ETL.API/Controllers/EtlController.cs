using ETL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETL.API.Controllers
{
    [ApiController]
    [Route("api/etl")]
    public class EtlController : ControllerBase
    {
        private readonly IEtlService etlService;

        public EtlController(IEtlService etlService)
        {
            this.etlService = etlService;
        }

        [HttpPost("start")]
        public IActionResult StartETL()
        {
            this.etlService.Start();
            return Ok("ETL Pipeline Completed");
        }
    }
}
