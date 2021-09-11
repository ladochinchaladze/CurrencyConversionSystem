using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Currencies.Queries
{
    public class CurrencyDto : IMapFrom<Currency>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ExchangeRateDto ExchangeRate { get; set; }

    }
}
