using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models
{
    public class OddNameMappedModel
    {
        public int OddId { get; set; }
        public string OddName { get; set; }
        public int BetId { get; set; }
        public string BetName { get; set; }
        public int BetImportance { get; set; }
        public int OddImportance { get; set; }
        public bool IsFixedBet { get; set; }
        public int BetNameType { get; set; }
        public string CustOddName { get; set; }
        public string CustSource { get; set; }
    }
}
