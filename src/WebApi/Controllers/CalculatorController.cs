using Application.Calculators.Commands;
using Application.Calculators.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CalculatorController : ApiControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<decimal>> GetCurrencyExchangeRate([FromQuery] GetCurrencyExchangeRateQuery query)
        {
            var data = await Mediator.Send(query);
            return data;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> ExchangeCurrency(ExchangeCurrencyCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
