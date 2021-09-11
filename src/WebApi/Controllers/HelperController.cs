using Application.Helpers.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class HelperController : ApiControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<PersonVM>> GetPerson([FromQuery] GetPersonQuery query)
        {
            var person = await Mediator.Send(query);
            return person;
        }
    }
}
