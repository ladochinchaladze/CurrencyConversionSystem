using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Calculators.Queries
{
    public class GetCurrencyExchangeRateQuery : IRequest<decimal>
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }


        public class GetCurrencyExchangeRateQueryHandler : IRequestHandler<GetCurrencyExchangeRateQuery, decimal>
        {
            private readonly IApplicationDbContext _context;

            public GetCurrencyExchangeRateQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<decimal> Handle(GetCurrencyExchangeRateQuery request, CancellationToken cancellationToken)
            {
                var currencyFrom = await _context
                    .Currencies
                    .Include(x => x.ExchangeRate)
                    .FirstAsync(x => x.Code == request.CurrencyFrom, cancellationToken);


                var currencyTo = await _context
                    .Currencies
                    .Include(x => x.ExchangeRate)
                    .FirstAsync(x => x.Code == request.CurrencyTo, cancellationToken);


                var exchangeRate = currencyTo.ExchangeRate.Sell / currencyFrom.ExchangeRate.Sell;

                return exchangeRate;
            }
        }
    }
}
