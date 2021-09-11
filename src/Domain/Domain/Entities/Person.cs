using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : AuditableEntity
    {
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RecomendatorIdentityNumber { get; set; }

        virtual public Person Recomendator { get; set; }
        virtual public IList<Person> Recommendeds { get; private set; } = new List<Person>();
        public IList<Conversion> Conversions { get; private set; } = new List<Conversion>();
    }
}
