using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons.Commands
{
    public class CreateRecommendatorCommandValidator : AbstractValidator<CreateRecommendatorCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateRecommendatorCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.IdentityNumber)
                .NotEmpty()
                .NotNull()
                .Length(11);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull();

            RuleFor(x => x.IdentityNumber)
                .MustAsync((o, IdentityNumber, CancellationToken) => { return IsUnique(IdentityNumber, CancellationToken); })
                .WithMessage("Person already exists");
        }


        public async Task<bool> IsUnique(string identityNumber, CancellationToken cancellationToken)
        {
            return await Task.FromResult(!_context.Persons.Any(x => x.IdentityNumber == identityNumber));
        }
    }
}
