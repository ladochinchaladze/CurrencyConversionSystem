using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Currency : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public bool IsDeleted { get; set; }

        public IList<Conversion> ConversionsForFrom { get; private set; } = new List<Conversion>();
        public IList<Conversion> ConversionsForTo { get; private set; } = new List<Conversion>();
        public ExchangeRate ExchangeRate { get; set; }
    }
}
