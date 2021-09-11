using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Calculators.Commands
{
    public class ExchangeCurrencyCommand : IRequest<Unit>
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RecomendatorIdentityNumber { get; set; }


        public class ExchangeCurrencyCommandhandler : IRequestHandler<ExchangeCurrencyCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public ExchangeCurrencyCommandhandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ExchangeCurrencyCommand request, CancellationToken cancellationToken)
            {

                var person = await _context
                    .Persons
                    .FirstOrDefaultAsync(x => x.IdentityNumber == request.IdentityNumber, cancellationToken);

                var personId = person?.Id;

                if (person == null)
                {
                    var addPerson = new Domain.Entities.Person
                    {
                        IdentityNumber = request.IdentityNumber,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        RecomendatorIdentityNumber = request.RecomendatorIdentityNumber
                    };

                    await _context
                        .Persons
                        .AddAsync(addPerson, cancellationToken);

                    personId = addPerson.Id;
                }

                var currencyFrom = await _context
                    .Currencies
                    .Include(x => x.ExchangeRate)
                    .FirstAsync(x => x.Code == request.CurrencyFrom, cancellationToken);

                var currencyTo = await _context
                    .Currencies
                    .Include(x => x.ExchangeRate)
                    .FirstAsync(x => x.Code == request.CurrencyTo, cancellationToken);

                await _context
                    .Conversions
                    .AddAsync(new Domain.Entities.Conversion
                    {
                        AmountInLari = currencyFrom.ExchangeRate.Sell * request.ReceivedAmount,
                        CreateDate = DateTime.Now,
                        CurrencyFromId = currencyFrom.Id,
                        CurrencyToId = currencyTo.Id,
                        ExchangeRate = request.ExchangeRate,
                        PersonId = personId,
                        ReceivedAmount = request.ReceivedAmount,
                        PaidAmount = request.PaidAmount
                    }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
