using Newtonsoft.Json;
using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models;
using SharpApiTester.Api.Models.Export;
using SharpApiTester.Api.Models.Import;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SharpApiTester
{
    public partial class UcCalcOdds : UserControl
    {
        private List<ExtendedMatchOddModel> calculatedMatchOdds;

        public UcCalcOdds()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbxProfitNames.ItemsSource = await MakeOddsCaller.GetProfitNames();
            cbxProfitNames.SelectedIndex = 0;
        }

        private async void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            ClearCalcDisplayControls();

            //Take parameters
            int favorite = 0;

            if ((bool)rbtnHomeFav.IsChecked)
                favorite = 1;
            else if ((bool)rbtnAwayFav.IsChecked)
                favorite = 2;

            string winnerOdd = tbxMatchFav.Text;
            string drawOdd = tbxDraw.Text;
            string goals3p = tbxTotalGoals3p.Text;
            ProfitNameModel profit = (ProfitNameModel)cbxProfitNames.SelectedItem;
            int profitId = profit.ProfitNameId;
            int calcType = cbxCalcSeries.SelectedIndex + 1;

            //Data Validation
            CalcValidator calcValidator = new CalcValidator();
            bool isOk = calcValidator.ValidateCalcInputs(favorite.ToString(), winnerOdd, drawOdd, goals3p, profitId.ToString(), calcType);

            if (isOk==false)
            {
                MessageBox.Show(calcValidator.ErrMessage, "Data Validation Error");
                return;
            }

            //Call localhost service
            calculatedMatchOdds = await MakeOddsCaller.GetCalcOdds(favorite, winnerOdd, drawOdd, goals3p, profitId, calcType);
            dtgOdds.ItemsSource = calculatedMatchOdds;
            //Put here implementation to transfer 'calculatedMatchOdds' collection to your Database, Excel, Service ...

            if (calculatedMatchOdds != null)
                tbkOddsCount.Text = $"Count: {calculatedMatchOdds.Count}";

            if (chbIsSorted.IsChecked == true)
                SortOdds();
        }

        private void ClearCalcDisplayControls()
        {
            tbkOddsCount.Text = string.Empty;
            dtgOdds.ItemsSource = null;
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

        private async void btnCalculateToSource_Click(object sender, RoutedEventArgs e)
        {
            ClearCalcDisplayControls();

            //Take parameters
            int favorite = 0;

            if ((bool)rbtnHomeFav.IsChecked)
                favorite = 1;
            else if ((bool)rbtnAwayFav.IsChecked)
                favorite = 2;

            string winnerOdd = tbxMatchFav.Text;
            string drawOdd = tbxDraw.Text;
            string goals3p = tbxTotalGoals3p.Text;
            int profitId = 0;
            int calcType = cbxCalcSeries.SelectedIndex + 1;
            string custSource = tbxSource.Text;

            //Data Validation
            CalcValidator calcValidator = new CalcValidator();
            bool isOk = calcValidator.ValidateCalcInputs(favorite.ToString(), winnerOdd, drawOdd, goals3p, profitId.ToString(), calcType);

            if (isOk == false)
            {
                MessageBox.Show(calcValidator.ErrMessage, "Data Validation Error");
                return;
            }

            //Call localhost service
            calculatedMatchOdds = await MakeOddsCaller.GetCalcOdds(favorite, winnerOdd, drawOdd, goals3p, profitId, calcType, custSource);
            dtgOdds.ItemsSource = calculatedMatchOdds;
            //Put here implementation to transfer 'calculatedMatchOdds' collection to your Database, Excel, Service ...

            if (calculatedMatchOdds != null)
                tbkOddsCount.Text = $"Count: {calculatedMatchOdds.Count}";

            if (chbIsSorted.IsChecked == true)
                SortOdds();
        }

        private async void btImportCalcOddsToSharpOdds_Click(object sender, RoutedEventArgs e)
        {
            //This method will export calculated odds
            if (calculatedMatchOdds == null)
            {
                MessageBox.Show("You haven't calculated any odds yet!");
                return;
            }

            //Bookmaker's Name
            string bookmakerName = tbxBookmakerName.Text;
            if (string.IsNullOrWhiteSpace(bookmakerName))
            {
                MessageBox.Show("Bookmaker's Name is required");
                return;
            }

            //Convert calculated odds into appropriate type
            CalcOddRootModel calcOddRoot = new CalcOddRootModel { Bookmaker = bookmakerName, MatchOddModels = new List<MatchOddModel>() };
            foreach (ExtendedMatchOddModel calcMatchOdd in calculatedMatchOdds)
                calcOddRoot.MatchOddModels.Add(new MatchOddModel {
                    OddId = calcMatchOdd.OddId,
                    BookMakerId = -1,
                    OddDec = calcMatchOdd.OddDec
                });

            //Call the localhost service
            var res = await MakeOddsCaller.LoadCalcMatchOddsToSharp(calcOddRoot);
            MessageBox.Show(res);
        }

        private async void btRefreshOddsUI_Click(object sender, RoutedEventArgs e)
        {
            var res = await RefreshUiCaller.RefreshUi();
            MessageBox.Show(res);
        }

        private void SortOdds()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dtgOdds.ItemsSource);

            if (view == null)
                return;

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
