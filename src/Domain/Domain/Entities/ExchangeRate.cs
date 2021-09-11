using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExchangeRate : AuditableEntity
    {
        public Guid Id { get; set; }
        public decimal Sell { get; set; }
        public decimal Buy { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CurrencyId { get; set; }

        public Currency Currency { get; set; }
    }
}
