using Application.Persons.Commands;
using Application.Persons.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class PersonsController : ApiControllerBase
    {
        [HttpGet("{identityNumber}")]
        public async Task<ActionResult<PersonVM>> Get(string identityNumber)
        {
            var person = await Mediator.Send(new GetPersonQuery { IdentityNumber = identityNumber });
            return person;
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<PersonVM>> CreateRecommendator(CreateRecommendatorCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
