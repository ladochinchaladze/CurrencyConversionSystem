using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Reports.Queries
{
    public class GetReportQuery : IRequest<ReportVM>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string IdentityNumber { get; set; }


        public class GetReportQueryHandler : IRequestHandler<GetReportQuery, ReportVM>
        {
            private readonly IApplicationDbContext _context;

            public GetReportQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ReportVM> Handle(GetReportQuery request, CancellationToken cancellationToken)
            {
                var ownConversions = await _context
                    .Conversions
                    .Include(x => x.Person)
                    .Where(x => x.Person.IdentityNumber == request.IdentityNumber &&
                        (request.From == null ? true : x.CreateDate >= request.From) &&
                        (request.To == null ? true : x.CreateDate <= request.To))
                    .CountAsync(cancellationToken);



                var allConversions = await _context
                    .Persons
                    .Include(x => x.Conversions.Where(c => (request.From == null ? true : c.CreateDate >= request.From)
                    && (request.To == null ? true : c.CreateDate <= request.To)))
                    .Where(x => x.RecommendatorIdentityNumber == request.IdentityNumber)
                    .Select(x => x.Conversions)
                    .CountAsync(cancellationToken);

                return new ReportVM
                {
                    AllConversions = allConversions + ownConversions,
                    OwnConversions = ownConversions,
                    IdentityNumber = request.IdentityNumber
                };

            }
        }
    }
}
