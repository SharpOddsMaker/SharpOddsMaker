using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models.Import;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SharpApiTester
{
    public partial class UcMatches : UserControl
    {
        private List<MatchRowModel> matchRowModels;

        public UcMatches()
        {
            InitializeComponent();
        }

        private async void btnGetSources_Click(object sender, RoutedEventArgs e)
        {
            var custSources = await MatchCaller.GetMatchSources();
            dtgSources.ItemsSource = custSources;
        }

        private void dtgSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSource = (string)dtgSources.SelectedItem;
            tbxSource.Text = selectedSource;
        }

        private async void btnGetSharpMatches_Click(object sender, RoutedEventArgs e)
        {
            //Clear controls
            dtgMatches.ItemsSource = null;
            tbkMatchCount.Text = string.Empty;
            tbxSearch.Clear();

            //Call the localhost service
            matchRowModels = await MatchCaller.GetSharpMatches(tbxSource.Text);

            //Put here implementation to transfer 'matchRowModels' collection to your Database, Excel, Service ...

            if (matchRowModels != null)
            {
                dtgMatches.ItemsSource = matchRowModels;
                tbkMatchCount.Text = $"Count: {matchRowModels.Count}";
            }
        }

        private void tbxSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (matchRowModels == null)
                return;
            
            string searchTerm = tbxSearch.Text.Trim().ToLower();

            if(string.IsNullOrEmpty(searchTerm))
            {
                dtgMatches.ItemsSource = matchRowModels;
                tbkMatchCount.Text = $"Count: {matchRowModels.Count}";
            }
            else
            {
                var searchedMatches = matchRowModels.Where(m => m.Home.ToLower().Contains(searchTerm) ||
                    m.Away.ToLower().Contains(searchTerm) || m.LeagueCountry.ToLower().Contains(searchTerm) ||
                    m.LeagueName.ToLower().Contains(searchTerm));

                dtgMatches.ItemsSource = searchedMatches;

                tbkMatchCount.Text = $"Count: {searchedMatches.Count()}";
            }
        }

        private void dtgMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MatchRowModel match = (MatchRowModel)dtgMatches.SelectedItem;
            ApiGlobal.SelectedMatch = match;

            if (match == null)
                return;

            tblFixtureId.Text = match.FixtureId.ToString();
            tblReferee.Text = match.Referee;
            tblStartDate.Text = match.StartDate.ToString("f");
            tblStatus.Text = match.StatusLong;
            tblLeagueSeason.Text = match.LeagueSeason.ToString();
            tblSeasonRound.Text = match.SeasonRound;
            tblVenueName.Text = match.VenueName;
            tblCity.Text = match.City;
            tblAwayId.Text = match.AwayId.ToString();
            tblHomeId.Text = match.HomeId.ToString();
            tblAwayName.Text = match.Away;
            tblHomeName.Text = match.Home;
            tblCustHomeName.Text = match.CustHome;
            tblCustAwayName.Text = match.CustAway;
            tblCustSourceName.Text = match.CustSource;
            tbkLeagueId.Text = match.LeagueId.ToString();
            tbkLeagueName.Text = match.LeagueName;
            tbkCategoryId.Text = match.CategoryId.ToString();
            tbkLeagueCountry.Text = match.LeagueCountry;
        }

        //Load selected match into SharpOddsMaker
        private async void btLoadMatchDetails_Click(object sender, RoutedEventArgs e)
        {
            string fixtureId = tblFixtureId.Text;

            if (fixtureId == "")
            {
                MessageBox.Show("A match must be selected!");
                return;
            }

            await MatchCaller.LoadMatchDetailsIntoSharp(fixtureId);
        }

        private async void btLoadOdds_Click(object sender, RoutedEventArgs e)
        {
            string fixtureId = tblFixtureId.Text;

            if (fixtureId == "")
            {
                MessageBox.Show("A match must be selected!");
                return;
            }

            await MatchCaller.LoadMatchOddsIntoSharp(fixtureId);
        }
    }
}
