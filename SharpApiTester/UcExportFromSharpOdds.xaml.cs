using Newtonsoft.Json;
using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models;
using SharpApiTester.Api.Models.Export;
using SharpApiTester.Api.Models.Import;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SharpApiTester
{
    public partial class UcExportFromSharpOdds : UserControl
    {
        private List<MatchRowModel> savedMatches;

        public UcExportFromSharpOdds()
        {
            InitializeComponent();
        }

        private async void btGetSavedSeesions_Click(object sender, RoutedEventArgs e)
        {
            List<SaveSessionModel> savedSessions = await ExportSavedFromSharpCaller.GetSessions();
            dtgSeassons.ItemsSource = savedSessions;

            //Put here implementation to transfer 'savedSessions' collection to your Database, Excel, Service ...

            if (savedSessions==null || savedSessions.Count==0)
                MessageBox.Show("There is No Saved Sessions.");
        }

        private void dtgSeassons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveSessionModel session = (SaveSessionModel)dtgSeassons.SelectedItem;

            if (session == null)
            {
                MessageBox.Show("Select a Session.");
                return;
            }

            tbxSessionId.Text = session.SessionID.ToString();
        }

        private async void btnGetMatchSources_Click(object sender, RoutedEventArgs e)
        {
            var custSources = await MatchCaller.GetMatchSources();
            dtgMatchesSources.ItemsSource = custSources;
        }

        private void dtgMatchesSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSource = (string)dtgMatchesSources.SelectedItem;
            tbxMatchSource.Text = selectedSource;
            tbxOddsSource.Text = selectedSource;
        }

        private async void btnGetOddSources_Click(object sender, RoutedEventArgs e)
        {
            List<string> custSources = await MatchOddsCaller.GetOddSources();
            dtgOddSources.ItemsSource = custSources;
        }

        private void dtgOddSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSource = (string)dtgOddSources.SelectedItem;
            tbxOddsSource.Text = selectedSource;
        }

        private async void btGetMatches_Click(object sender, RoutedEventArgs e)
        {
            ClearMatchControls();

            //Take Parameters
            string sessionIdStr = tbxSessionId.Text.Trim();
            if (string.IsNullOrWhiteSpace(sessionIdStr))
            {
                MessageBox.Show("SessionId is required");
                return;
            }

            string matchSource = tbxMatchSource.Text.Trim();
            if (string.IsNullOrEmpty(matchSource))
                matchSource = null;

            if (int.TryParse(sessionIdStr, out int sessionId))
            {
                //Export all matches from Sharp Odds Maker for given session ID
                savedMatches = await ExportSavedFromSharpCaller.GetSavedMatches(sessionId, matchSource);
                dtgMatches.ItemsSource = savedMatches;
                tbkMatchesCount.Text = $"Match Count: {savedMatches.Count}";

                //Put here implementation to transfer 'savedMatches' collection to your Database, Excel, Service ...
            }
            else
                MessageBox.Show("SessionId should be number.");
        }

        private void ClearMatchControls()
        {
            tbkMatchesCount.Text = string.Empty;
            dtgMatches.ItemsSource = null;
            tbxSearchMatch.Clear();
        }

        private void tbxSearchMatch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (savedMatches==null)
                return;

            string searchTerm = tbxSearchMatch.Text.Trim().ToLower();
            tbkMatchesCount.Text = string.Empty;

            if (string.IsNullOrEmpty(searchTerm))
            {
                dtgMatches.ItemsSource = savedMatches;
                tbkMatchesCount.Text = $"Match Count: {savedMatches.Count}";
            }
            else
            {
                var searchedMatches = savedMatches.Where(m => m.Home.ToLower().Contains(searchTerm) ||
                    m.Away.ToLower().Contains(searchTerm) || m.LeagueCountry.ToLower().Contains(searchTerm) ||
                    m.LeagueName.ToLower().Contains(searchTerm));

                dtgMatches.ItemsSource = searchedMatches;
                tbkMatchesCount.Text = $"Match Count: {searchedMatches.Count()}";
            }
        }


        private async void dtgMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Clear controls
            ClearOddsControls();

            MatchRowModel match = (MatchRowModel)dtgMatches.SelectedItem;
            string oddsSource = tbxOddsSource.Text.Trim();
            if (oddsSource == "")
                oddsSource = null;

            if (match == null)
            {
                MessageBox.Show("Select a match.");
                return;
            }

            BindMatchDetails(match);

            //Get Saved Odds from localhost service
            List<MatchOddWithNamesModel> savedOdds = await ExportSavedFromSharpCaller.GetOdds4SelectedMatch(match.SessionId, match.FixtureId, oddsSource);
            dtgOdds.ItemsSource = savedOdds;

            //Put here implementation to transfer 'savedOdds' collection to your Database, Excel, Service ...

            //Counters
            if (savedOdds != null)
            {
                tbkOddsCount.Text = $"Odds Count: {savedOdds.Count.ToString()}";
                tbkUniqueOddsCount.Text = $"Unique Odds Count: {savedOdds.Select(o => o.OddId).Distinct().Count()}";
            }

            if (chbIsSorted.IsChecked == true)
                SortOdds();
        }

        private void ClearOddsControls()
        {
            dtgOdds.ItemsSource = null;
            tbkOddsCount.Text = string.Empty;
            tbkUniqueOddsCount.Text = string.Empty;
        }

        private void BindMatchDetails(MatchRowModel match)
        {
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
            tblCategory.Text = match.LeagueCountry;
            tblLeague.Text = match.LeagueName;
            tblCategoryId.Text = match.CategoryId.ToString();
            tblLeagueId.Text = match.LeagueId.ToString();
            tblCustSource.Text = match.CustSource;
            tblCustHome.Text = match.CustHome;
            tblCustAway.Text = match.CustAway;
        }

        private void SortOdds()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dtgOdds.ItemsSource);
            view.SortDescriptions.Clear();
            SortDescription sd = new SortDescription("BetImportance", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);
            sd = new SortDescription("OddImportance", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);
            sd = new SortDescription("OddImportance", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);
            sd = new SortDescription("OddName", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);
            sd = new SortDescription("BookmakerName", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);
        }
    }
}
