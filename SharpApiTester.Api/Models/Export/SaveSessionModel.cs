using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models.Export
{
    public class SaveSessionModel
    {
        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public string Nick { get; set; }
        public int MatchCount { get; set; }
        public DateTime MinStartDate { get; set; }
        public DateTime MaxStartDate { get; set; }
    }
}
