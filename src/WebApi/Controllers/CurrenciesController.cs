using Application.Currencies.Commands;
using Application.Currencies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CurrenciesController : ApiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<CurrencyDto>>> Get()
        {
            return await Mediator.Send(new GetCurrenciesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyDto>> Get(Guid id)
        {
            return await Mediator.Send(new GetCurrencyQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateCurrencyCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateCurrencyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteCurrencyComand { Id = id });
            return Ok();
        }

    }
}
