using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Currencies.Commands
{
    public class DeleteCurrencyComand : IRequest<Unit>
    {
        public Guid Id { get; set; }


        public class DeleteCurrencyComandhandler : IRequestHandler<DeleteCurrencyComand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCurrencyComandhandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCurrencyComand request, CancellationToken cancellationToken)
            {
                var currency = await _context
                    .Currencies
                    .Include(x => x.ExchangeRate)
                    .FirstAsync(x => x.Id == request.Id, cancellationToken);

                currency.IsDeleted = true;
                currency.ExchangeRate.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
