using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.Models.Import
{
    public class MatchRowModel
    {
        public int FixtureId { get; set; }
        public int SportId { get; set; }
        public int SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public int LeagueSeason { get; set; }
        public string SeasonRound { get; set; }
        public int HomeId { get; set; }
        public string Home { get; set; }
        public string HomeLogo { get; set; }
        public int AwayId { get; set; }
        public string Away { get; set; }
        public string AwayLogo { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public int CategoryId { get; set; }
        public string LeagueCountry { get; set; }
        public string VenueName { get; set; }
        public string City { get; set; }
        public string Referee { get; set; }
        public string StatusLong { get; set; }
        public DateTime OddsUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public int TimeStampFixature { get; set; }
        public string UserMatchKey { get; set; }
        public string CustHome { get; set; }
        public string CustAway { get; set; }
        public string CustSource { get; set; }
    }
}
