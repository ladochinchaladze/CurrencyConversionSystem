using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reports.Queries
{
    public class ReportVM
    {
        public string IdentityNumber { get; set; }
        public int OwnConversions { get; set; }
        public int AllConversions { get; set; }
    }
}
