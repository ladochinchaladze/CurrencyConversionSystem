

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Currency> Currencies { get; set; }
        DbSet<ExchangeRate> ExchangeRates { get; set; }
        DbSet<Conversion> Conversions { get; set; }
        DbSet<Person> Persons { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
