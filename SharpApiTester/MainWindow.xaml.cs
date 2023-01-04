using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SharpApiTester
{
    public partial class MainWindow : Window
    {
        private readonly UcLogin ucLogin;
        private readonly UcMatches ucMatches;
        private readonly UcOdds ucOdds;
        private readonly UcCalcOdds ucCalcOdds;
        private readonly UcExportFromSharpOdds ucImportMatchesAndOdds;
        private readonly UcImportFromSharpOdds ucExportMatchesAndOdds;
        private readonly UcDeleteOdds ucDeleteOdds;
        private readonly UcSettings ucSettings;
        private readonly UcOddNames ucOddNames;

        public MainWindow()
        {
            InitializeComponent();

            ucLogin = new UcLogin();
            ucMatches = new UcMatches();
            ucOdds = new UcOdds();
            ucCalcOdds = new UcCalcOdds();
            ucImportMatchesAndOdds = new UcExportFromSharpOdds();
            ucExportMatchesAndOdds = new UcImportFromSharpOdds();
            ucDeleteOdds = new UcDeleteOdds();
            ucSettings = new UcSettings();
            ucOddNames = new UcOddNames();

            ApiGlobal.PortNumber = ucSettings.tbxPortNumber.Text;
            tbiSettings.Focus();
        }

        //Logic for changing tabs
        private void tbcTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tbiSettings.IsSelected)
                    NavigatePage(ucSettings, EnActivePage.Settings);
                else if (tbiLogin.IsSelected)
                    NavigatePage(ucLogin, EnActivePage.Login);
                else if (tbiOddNames.IsSelected)
                    NavigatePage(ucOddNames, EnActivePage.OddNames);
                else if(tbiMatches.IsSelected)
                    NavigatePage(ucMatches, EnActivePage.Matches);
                else if (tbiOdds.IsSelected)
                {
                    NavigatePage(ucOdds, EnActivePage.Odds);
                    if (ApiGlobal.SelectedMatch != null)
                        ucOdds.tblMatchName.Text = $"{ApiGlobal.SelectedMatch.Home} - {ApiGlobal.SelectedMatch.Away}";
                }
                else if (tbiCalcOdds.IsSelected)
                    NavigatePage(ucCalcOdds, EnActivePage.CalculateOdds);
                else if (tbiImportMatchesAndOdds.IsSelected)
                    NavigatePage(ucImportMatchesAndOdds, EnActivePage.ImportMatchesAndOdds);
                else if (tbiExportMatchesAndOdds.IsSelected)
                    NavigatePage(ucExportMatchesAndOdds, EnActivePage.ExportMatchesAndOdds);
                else if (tbiDeleteOdds.IsSelected)
                    NavigatePage(ucDeleteOdds, EnActivePage.DeleteOdds);
            }
        }

        //This method switches the user control that is displayed on grMain
        private void NavigatePage(UserControl uc, EnActivePage enActivePage)
        {
            if (ApiGlobal.ActivePage == enActivePage)
                return;

            grMain.Children.Clear();
            grMain.Children.Add(uc);
            ApiGlobal.ActivePage = enActivePage;
        }
    }
}
