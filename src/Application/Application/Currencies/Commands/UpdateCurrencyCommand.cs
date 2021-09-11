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
    public class UpdateCurrencyCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public class UpdateCurrencyCommandHendler : IRequestHandler<UpdateCurrencyCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public UpdateCurrencyCommandHendler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
            {
                var currency = await _context
                    .Currencies
                    .FirstAsync(x => x.Id == request.Id, cancellationToken);

                currency.Code = request.Code;
                currency.Name = request.Name;
                currency.NameEng = request.NameEng;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
