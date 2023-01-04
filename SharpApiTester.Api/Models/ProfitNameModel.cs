using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models
{
    public class ProfitNameModel
    {
        public int ProfitNameId { get; set; }
        public string Name { get; set; }
        public bool IsAllEqual { get; set; }
        public decimal DefaultValue { get; set; } = 10.0m;
    }
}
