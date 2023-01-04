using Newtonsoft.Json;
using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public partial class UcDeleteOdds : UserControl
    {
        public UcDeleteOdds()
        {
            InitializeComponent();
        }

        private async void btGetBookmakers_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, BookmakerModel> bookmakers = await BookmakerCaller.GetBookmakers();
            if(bookmakers!=null)
                lvBookmakers.ItemsSource = bookmakers.Values;
        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            BookmakerModel bookmaker = (BookmakerModel)lvBookmakers.SelectedItem;

            if (bookmaker == null)
            {
                MessageBox.Show("Bookmaker is Required.");
                return;
            }

            foreach (BookmakerModel bookMaker in lvBookmakers.SelectedItems)
            {
                BookmakerModel model = (BookmakerModel)bookMaker;
                //Call localhost service
                await DeleteOddsCaller.DeleteBookmakerOdds(bookMaker.BookMakerId);
            }
        }

        private async void btRefreshOddsUI_Click(object sender, RoutedEventArgs e)
        {
            var res = await RefreshUiCaller.RefreshUi();
            MessageBox.Show(res);
        }

        private async void btDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            var res = await DeleteOddsCaller.RemoveAll();
            MessageBox.Show(res);
        }
    }
}
