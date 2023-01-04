using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models
{
    public class ExtendedMatchOddModel
    {
        public int BetId { get; set; }
        public string BetName { get; set; }
        public int OddId { get; set; }
        public string OddName { get; set; }
        public int BetImportance { get; set; }
        public int OddImportance { get; set; }
        public string CustOddName { get; set; }
        public string CustSource { get; set; }
        public float OddDec { get; set; }
    }
}
