using SharpApiTester.Api.Models;
using SharpApiTester.Api.Models.Import;
using System.Collections.Generic;

namespace SharpApiTester.Api
{
    public enum EnActivePage { Settings =1, Login = 2, OddNames=3, Matches =4, Odds=5, CalculateOdds=6, ImportMatchesAndOdds=7, ExportMatchesAndOdds=8, DeleteOdds=9 }
    
    public static class ApiGlobal
    {
        public static string PortNumber { get; set; }
        public static string ServiceAddress => $"http://localhost:{PortNumber}";
        public static EnActivePage ActivePage { get; set; }
        public static bool ParamOddsOnly { get; set; } = false;

        public static MatchRowModel SelectedMatch { get; set; }
    }
}
