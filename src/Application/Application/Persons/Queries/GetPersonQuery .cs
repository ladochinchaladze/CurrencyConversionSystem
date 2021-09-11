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

namespace Application.Persons.Queries
{
    public class GetPersonQuery : IRequest<PersonVM>
    {
        public string IdentityNumber { get; set; }

        public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PersonVM> Handle(GetPersonQuery request, CancellationToken cancellationToken)
            {
                var personVM = await _context
                    .Persons
                    .AsNoTracking()
                    .ProjectTo<PersonVM>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.IdentityNumber == request.IdentityNumber, cancellationToken);

                return personVM;
            }
        }
    }
}
