using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GlobalErrorsController : ControllerBase
    {
        [HttpGet]
        [Route("/errors")]
        public IActionResult HandleErrors()
        {
            return Problem("Something went wrong");
        }
    }
}
