using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpApiTester
{

    public partial class UcOddNames : UserControl
    {
        private List<OddNameMappedModel> oddNames;

        public UcOddNames()
        {
            InitializeComponent();
        }

        private async void btnGetSources_Click(object sender, RoutedEventArgs e)
        {
            dtgSources.ItemsSource = null;
            List<string> custSources = await MatchOddsCaller.GetOddSources();
            dtgSources.ItemsSource = custSources;
        }

        private void dtgSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSource = (string)dtgSources.SelectedItem;
            tbxSource.Text = selectedSource;
        }

        private async void btGetOddNames_Click(object sender, RoutedEventArgs e)
        {
            string custSource = tbxSource.Text.Trim();
            if (custSource == string.Empty)
                custSource = null;

            //Clear Search Controls
            cbxBetType.SelectedIndex = 0;
            tbxSearchBets.Clear();

            dtgOddNames.ItemsSource = null;
            oddNames  = await MatchOddsCaller.GetOddNames(custSource);
            dtgOddNames.ItemsSource = oddNames;

            SortOddNames();
            tbkOddNameCount.Text = $"Count: {oddNames.Count}";
        }

        private void cbxBetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string searchTerm = tbxSearchBets.Text.Trim().ToLower();
            int betNameType = cbxBetType.SelectedIndex;

            SearchOddNames(searchTerm, betNameType);
        }

        private void tbxSearchBets_KeyUp(object sender, KeyEventArgs e)
        {
            string searchTerm = tbxSearchBets.Text.Trim().ToLower();
            int betNameType = cbxBetType.SelectedIndex;

            SearchOddNames(searchTerm, betNameType);
        }

        private void SearchOddNames(string searchTerm, int betNameType)
        {
            if (oddNames == null)
                return;

            if (string.IsNullOrEmpty(searchTerm) && betNameType==0)
            {
                dtgOddNames.ItemsSource = oddNames;
                tbkOddNameCount.Text = $"Count: {oddNames.Count}";
            }
            else if(!string.IsNullOrEmpty(searchTerm) && betNameType == 0)
            {
                var searchedMatches = oddNames.Where(m => (m.BetName.ToLower().Contains(searchTerm) ||
                    m.OddName.ToLower().Contains(searchTerm) || (m.CustOddName != null && m.CustOddName.ToLower().Contains(searchTerm))));

                dtgOddNames.ItemsSource = searchedMatches;

                tbkOddNameCount.Text = $"Count: {searchedMatches.Count()}";
            }
            else
            {
                var searchedMatches = oddNames.Where(m => m.BetNameType==betNameType && (m.BetName.ToLower().Contains(searchTerm) ||
                    m.OddName.ToLower().Contains(searchTerm) || (m.CustOddName != null && m.CustOddName.ToLower().Contains(searchTerm))));

                dtgOddNames.ItemsSource = searchedMatches;

                tbkOddNameCount.Text = $"Count: {searchedMatches.Count()}";
            }
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            SortOddNames();
        }

        private void SortOddNames()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dtgOddNames.ItemsSource);

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
        }
    }
}
