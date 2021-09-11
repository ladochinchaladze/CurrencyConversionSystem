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
    public class ExchangeRateDto : IMapFrom<ExchangeRate>
    {
        public decimal Sell { get; set; }
        public decimal Buy { get; set; }

    }
}
