using SharpApiTester.Api;
using SharpApiTester.Api.ApiCallers;
using SharpApiTester.Api.Models;
using SharpApiTester.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class UcLogin : UserControl
    {
        public UcLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string errMessage = "";

            //Check if the login data is correct
            bool isCorrect = CheckCredentials(ref errMessage, tbxEmail.Text, tbxPassword.Password);

            if (isCorrect == false)
            {
                MessageBox.Show(errMessage);
                return;
            }

            //Call the localhost service
            await LogInCaller.LogIn(tbxEmail.Text, tbxPassword.Password);
        }

        private bool CheckCredentials(ref string errorMessage, string email, string password)
        {
            bool isOk = true;
            errorMessage = "";

            if (email.Length == 0)
                AddErrMessage(ref isOk, ref errorMessage, "Please enter Email");
            else if (IsValidEMail(email) == false)
                AddErrMessage(ref isOk, ref errorMessage, "Not a valid Email address");
            else if (email.Length < 4)
                AddErrMessage(ref isOk, ref errorMessage, "Email is too short");

            if (password.Length == 0)
                AddErrMessage(ref isOk, ref errorMessage, "Please enter Password");
            else if (password.Length < 8)
                AddErrMessage(ref isOk, ref errorMessage, "Password is too short. Please enter 8 charcters minimum with at least one non alpha-numeric character and at least one upper-case letter.");

            return isOk;
        }

        private void AddErrMessage(ref bool isOk, ref string errorMessage, string err)
        {
            isOk = false;

            errorMessage += err + Environment.NewLine;
        }

        private bool IsValidEMail(string email)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(email, pattern))
                return true;
            else
                return false;
        }
    }
}
