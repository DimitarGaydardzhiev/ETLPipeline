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
            var transactions = this.etlService.Start();
            return Ok(transactions);
        }

        [HttpPost("clear")]
        public IActionResult ClearData()
        {
            this.etlService.ClearData();
            return Ok();
        }
    }
}
