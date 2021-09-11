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
    public class UpdateExchangeRateCommandValidator : AbstractValidator<UpdateExchangeRateCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateExchangeRateCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.CurrencyId)
                .MustAsync(RateMustNotBeNull)
                .WithMessage("Exchange rate is not established");

        }

        public async Task<bool> RateMustNotBeNull(Guid currencyId, CancellationToken cancellationToken)
        {
            var rate = await _context
                .ExchangeRates
                .FirstOrDefaultAsync(x => x.CurrencyId == currencyId && x.IsDeleted == false, cancellationToken);

            return rate != null;
        }
    }
}
