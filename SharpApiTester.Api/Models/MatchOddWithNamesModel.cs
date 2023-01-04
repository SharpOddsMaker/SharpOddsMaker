using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models
{
    public class MatchOddWithNamesModel
    {
        public int FixtureId { get; set; }
        public int BetId { get; set; }
        public string BetName { get; set; }
        public int OddId { get; set; }
        public string OddName { get; set; }
        public string CustOddName { get; set; }
        public string CustSource { get; set; }
        public int BookmakerId { get; set; }
        public string BookmakerName { get; set; }
        public int BetImportance { get; set; }
        public int OddImportance { get; set; }
        public float OddDec { get; set; }
        public string OddColor { get; set; }
    }
}
