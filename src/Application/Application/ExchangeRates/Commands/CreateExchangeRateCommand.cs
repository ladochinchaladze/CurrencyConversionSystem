using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ExchangeRates.Commands
{
    public class CreateExchangeRateCommand : IRequest<Guid>
    {
        public decimal Sell { get; set; }
        public decimal Buy { get; set; }
        public Guid CurrencyId { get; set; }


        public class CreateExchangeRateCommandHandler : IRequestHandler<CreateExchangeRateCommand, Guid>
        {
            private readonly IApplicationDbContext _context;

            public CreateExchangeRateCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateExchangeRateCommand request, CancellationToken cancellationToken)
            {
                var rate = new ExchangeRate
                {
                    Sell = request.Sell,
                    Buy = request.Buy,
                    IsDeleted = false,
                    CurrencyId = request.CurrencyId
                };

                await _context.ExchangeRates.AddAsync(rate, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return rate.Id;
            }
        }
    }
}
