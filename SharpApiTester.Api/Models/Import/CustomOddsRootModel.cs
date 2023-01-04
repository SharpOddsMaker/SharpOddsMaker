using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models.Export
{
    public class CustomOddsRootModel
    {
        public List<CustomOddsModel> CustImpOddsModel { get; set; }
        public string BookMaker { get; set; }
    }
}
