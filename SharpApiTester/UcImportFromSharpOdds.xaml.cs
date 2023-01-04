using Newtonsoft.Json;
using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models.Export;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
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
    //Export matches and odds from this application into SharpOddsMaker

    public partial class UcImportFromSharpOdds : UserControl
    {
        private List<CustomMatch> matches;
        private List<CustomOddsModel> matchOdds;

        public UcImportFromSharpOdds()
        {
            InitializeComponent();
        }

        #region Matches
        private void btnGetMatches_Click(object sender, RoutedEventArgs e)
        {
            //You can load matches from your system here from database, service, csv file or whatever
            //We simulate this by loading matches from CSV File
            matches = GetMatchesFromCSVFile("TextFiles/MatchesForImportToSharpOdds.csv");

            dtgMatces.ItemsSource = matches;
        }

        private async void btImportMatchesToSharpOdds_Click(object sender, RoutedEventArgs e)
        {
            if (matches == null)
            {
                MessageBox.Show("First Get Matches from CSV file.");
                return;
            }

            //Call the localhost service
            await ImportToSharpCaller.ImportMatches(matches);
        }


        private List<CustomMatch> GetMatchesFromCSVFile(string path)
        {
            var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select TransformToMatch(line);

            return query.ToList();
        }

        private CustomMatch TransformToMatch(string line)
        {
            var column = line.Split(';');

            return new CustomMatch {
                CustMatchId = column[0],
                CustHome = column[1],
                CustAway = column[2],
                CustMatchDay = Int32.TryParse(column[3], out var custMatchDay) ? custMatchDay : (int?)null,
                CustCategory = column[4]
            };
        }
        #endregion



        #region Odds

        private void btnGetOdds_Click(object sender, RoutedEventArgs e)
        {
            matchOdds = GetOddsFromCSVFile("TextFiles/OddsForImportToSharpOdds.csv");
            dtgOdds.ItemsSource = matchOdds;
        }

        private List<CustomOddsModel> GetOddsFromCSVFile(string path)
        {
            var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select TransformToOdd(line);

            return query.ToList();
        }

        private CustomOddsModel TransformToOdd(string line)
        {
            var column = line.Split(';');

            return new CustomOddsModel {
                CustomersOddName = column[0],
                CustomerOdd = decimal.TryParse(column[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var decOdd) ? decOdd : 0
            };
        }

        private async void btnImportOddsToSharpOdds_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxBookmaker.Text))
            {
                MessageBox.Show("BookMaker is Required.");
                return;
            }
            else if (matchOdds == null)
            {
                MessageBox.Show("First Get Odds from CSV file.");
                return;
            }

            CustomOddsRootModel customOddsRoot = new CustomOddsRootModel { BookMaker = tbxBookmaker.Text?.ToString().Trim() };
            customOddsRoot.CustImpOddsModel = matchOdds;

            //Call the localhost service
            await ImportToSharpCaller.ImportOdds(customOddsRoot);
        }
        #endregion

    }
}
