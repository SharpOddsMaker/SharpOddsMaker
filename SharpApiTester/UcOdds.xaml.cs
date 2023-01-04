using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SharpApiTester
{
    public partial class UcOdds : UserControl
    {
        public UcOdds()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiGlobal.SelectedMatch == null)
            {
                tblMatchName.Text = "First you need to Select a Match on the Matches tab.";
                return;
            }
            else
            {
                tbxFixtureId.Text = ApiGlobal.SelectedMatch.FixtureId.ToString();
                tblMatchName.Text = $"{ApiGlobal.SelectedMatch.Home} - {ApiGlobal.SelectedMatch.Away}";
            }
        }

        private async void btnGetSources_Click(object sender, RoutedEventArgs e)
        {
            List<string> custSources = await MatchOddsCaller.GetOddSources();
            dtgSources.ItemsSource = custSources;
        }

        private void dtgSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSource = (string)dtgSources.SelectedItem;
            tbxSource.Text = selectedSource;
        }

        private async void btnGetAllOdds_Click(object sender, RoutedEventArgs e)
        {
            string fixtureIdStr = tbxFixtureId.Text.Trim();
            ClearOddsControls();

            if (string.IsNullOrWhiteSpace(fixtureIdStr))
            {
                MessageBox.Show("FixtureId is Required. Select Match on Matches TAB or input it in Text box.");
                return;
            }

            if (int.TryParse(fixtureIdStr, out int fixtureId))
            {
                string custSource;
                if (string.IsNullOrEmpty(tbxSource.Text))
                    custSource = null;
                else
                    custSource = tbxSource.Text.Trim();

                if (ApiGlobal.SelectedMatch.FixtureId == fixtureId)
                    tblMatchName.Text = $"{ApiGlobal.SelectedMatch.Home} - {ApiGlobal.SelectedMatch.Away}";
                else
                    tblMatchName.Text = "";

                List<MatchOddWithNamesModel> matchOdds = await MatchOddsCaller.GetAllMatchOdds(fixtureId, custSource);
                dtgOdds.ItemsSource = matchOdds;
                //Put here implementation to transfer 'matchOdds' collection to your Database, Excel, Service ...

                if (matchOdds != null)
                {
                    tbkOddsCount.Text = $"Odds Count: {matchOdds.Count}";
                    tbkUniqueOddsCount.Text = $"Unique odds Count: {matchOdds.Select(o => o.OddId).Distinct().Count()}";
                }

                if (chbIsSorted.IsChecked == true)
                    SortOdds();
            }
            else
                MessageBox.Show("FixtureId should be a number.");
        }

        private void ClearOddsControls()
        {
            tbkOddsCount.Text = string.Empty;
            tbkUniqueOddsCount.Text = string.Empty;
            dtgOdds.ItemsSource = null;
        }

        private async void btnGetNotCalcOdds_Click(object sender, RoutedEventArgs e)
        {
            string fixtureIdStr = tbxFixtureId.Text.Trim();
            ClearOddsControls();

            if (string.IsNullOrWhiteSpace(fixtureIdStr))
            {
                MessageBox.Show("FixtureId is Required. Select Match on Matches TAB or input it in Text box.");
                return;
            }

            if (int.TryParse(fixtureIdStr, out int fixtureId))
            {
                string custSource;
                if (string.IsNullOrWhiteSpace(tbxSource.Text))
                    custSource = null;
                else
                    custSource = tbxSource.Text;

                tblMatchName.Text = $"{ApiGlobal.SelectedMatch.Home} - {ApiGlobal.SelectedMatch.Away}";
                List<MatchOddWithNamesModel> matchOdds = await MatchOddsCaller.GetNotCalcOdds(fixtureId, custSource);
                dtgOdds.ItemsSource = matchOdds;
                //Put here implementation to transfer 'matchOdds' collection to your Database, Excel, Service ...

                if (matchOdds != null)
                {
                    tbkOddsCount.Text = $"Count: {matchOdds.Count}";
                    tbkUniqueOddsCount.Text = $"Unique odds Count: {matchOdds.Select(o => o.OddId).Distinct().Count()}";
                }

                if (chbIsSorted.IsChecked == true)
                    SortOdds();
            }
            else
                MessageBox.Show("FixtureId should be a number.");
        }

        private void SortOdds()
        {
            if (dtgOdds.ItemsSource == null)
                return;
            
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
