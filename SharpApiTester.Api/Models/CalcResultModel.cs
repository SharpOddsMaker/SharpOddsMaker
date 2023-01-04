using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models
{
    public class CalcResultModel
    {
        public List<ExtendedMatchOddModel> Odds { get; set; }
        public string Error { get; set; }
        public string Warning { get; set; }
        public string MatchKey { get; set; }
    }
}
