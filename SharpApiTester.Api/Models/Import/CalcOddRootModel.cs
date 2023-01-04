using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models.Import
{
    public class CalcOddRootModel
    {
        public string Bookmaker { get; set; }
        public List<MatchOddModel> MatchOddModels { get; set; }
    }
}
