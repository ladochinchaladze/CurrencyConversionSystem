using Application.Common.Interfaces;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Currencies.Commands
{
    public class CreateCurrencyCommand : IRequest<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public ExchangeRateModel ExchangeRate { get; set; }

        public class CreateCurrencyCommandHendler : IRequestHandler<CreateCurrencyCommand, Guid>
        {
            private readonly IApplicationDbContext _context;

            public CreateCurrencyCommandHendler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
            {
                var currency = new Domain.Entities.Currency
                {
                    Code = request.Code,
                    Name = request.Name,
                    NameEng = request.NameEng,
                    IsDeleted = false,

                    ExchangeRate = request.ExchangeRate != null ? new Domain.Entities.ExchangeRate
                    {
                        Sell = request.ExchangeRate.Sell,
                        Buy = request.ExchangeRate.Buy
                    } : null
                };

                await _context
                    .Currencies
                    .AddAsync(currency, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return currency.Id;
            }
        }
    }
}
