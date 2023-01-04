using SharpApiTester.Api;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for UcSettings.xaml
    /// </summary>
    public partial class UcSettings : UserControl
    {
        public UcSettings()
        {
            InitializeComponent();
        }

        private void tbxPortNumber_KeyUp(object sender, KeyEventArgs e)
        {
            ApiGlobal.PortNumber = tbxPortNumber.Text;
        }

        private void chbParamOdds_Click(object sender, RoutedEventArgs e)
        {
            ApiGlobal.ParamOddsOnly = chbParamOdds.IsChecked.GetValueOrDefault();
        }
    }
}
