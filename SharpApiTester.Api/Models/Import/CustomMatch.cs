using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models.Export
{
    public class CustomMatch
    {
        public string CustMatchId { get; set; }
        public string CustHome { get; set; }
        public string CustAway { get; set; }
        public int? CustMatchDay { get; set; }  //Optional
        public string CustCategory { get; set; } //Optional
    }
}
