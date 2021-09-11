using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ExchangeRates.Commands
{
    class CreateExchangeRateCommandValidator : AbstractValidator<CreateExchangeRateCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateExchangeRateCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.CurrencyId)
                .MustAsync(RateMustBeNull)
                .WithMessage("Exchange rate is already established")
                .MustAsync(CurrencyMustExist)
                .WithMessage("No Currency to be found");


        }

        public async Task<bool> RateMustBeNull(Guid currencyId, CancellationToken cancellationToken)
        {
            var rate = await _context
                .ExchangeRates
                .FirstOrDefaultAsync(x => x.CurrencyId == currencyId && x.IsDeleted == false, cancellationToken);

            return rate == null;
        }
        
        public async Task<bool> CurrencyMustExist(Guid currencyId, CancellationToken cancellationToken)
        {
            var currency = await _context
                .Currencies
                .FirstOrDefaultAsync(x => x.Id == currencyId && x.IsDeleted == false, cancellationToken);

            return currency == null;
        }

    }
}
