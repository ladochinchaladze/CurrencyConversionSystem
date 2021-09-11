using Application.ExchangeRates.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ExchangeRateController : ApiControllerBase
    {
        //TODO: Create CRUD;---------------------


        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateExchangeRateCommand command)
        {
            return await Mediator.Send(command);
        }


        [HttpPut("{currencyId}")]
        public async Task<ActionResult> Update(Guid currencyId, UpdateExchangeRateCommand command)
        {
            if (currencyId != command.CurrencyId)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
