using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Currencies.Queries
{
    public class GetCurrencyQuery : IRequest<CurrencyDto>
    {
        public Guid Id { get; set; }

        public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrencyQuery, CurrencyDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCurrenciesQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CurrencyDto> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
            {


                var currency = await _context
                    .Currencies
                    .Where(x => x.Id == request.Id && x.IsDeleted == false)
                    .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
                    .FirstAsync(cancellationToken);

                return currency;
            }
        }

    }
}
