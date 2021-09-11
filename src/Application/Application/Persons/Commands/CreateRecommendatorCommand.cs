using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Commands
{
    public class CreateRecommendatorCommand : IRequest<Unit>
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public class CreateRecommendatorCommandHandler : IRequestHandler<CreateRecommendatorCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateRecommendatorCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateRecommendatorCommand request, CancellationToken cancellationToken)
            {
                await _context
                    .Persons
                    .AddAsync(new Domain.Entities.Person
                    {
                        IdentityNumber = request.IdentityNumber,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        RecommendatorIdentityNumber = "00000000000"
                    }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
