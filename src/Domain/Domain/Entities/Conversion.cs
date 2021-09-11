using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Conversion
    {
        public Guid Id { get; set; }
        public Guid CurrencyFromId { get; set; }
        public Guid CurrencyToId { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal AmountInLari { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? PersonId { get; set; }

        public Currency CurrencyFrom { get; set; }
        public Currency CurrencyTo { get; set; }
        public Person Person { get; set; }


    }
}
