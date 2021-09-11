using Application.Reports.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ReportsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReportVM>> GetReport([FromQuery] GetReportQuery query)
        {
            return await Mediator.Send(new GetReportQuery());
        }
    }
}
