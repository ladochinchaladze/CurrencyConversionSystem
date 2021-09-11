using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ExchangeRates.Commands
{
    public class UpdateExchangeRateCommand : IRequest
    {
        public Guid CurrencyId { get; set; }
        public decimal Sell { get; set; }
        public decimal Buy { get; set; }

        public class UpdateExchangeRateCommandHendler : IRequestHandler<UpdateExchangeRateCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateExchangeRateCommandHendler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateExchangeRateCommand request, CancellationToken cancellationToken)
            {
                var rate = await _context
                    .ExchangeRates
                    .FirstAsync(x => x.CurrencyId == request.CurrencyId, cancellationToken);

                rate.Sell = request.Sell;
                rate.Buy = request.Buy;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
