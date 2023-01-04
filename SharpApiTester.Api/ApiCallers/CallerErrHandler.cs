using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpApiTester.Api.ApiCallers
{
    public static class CallerErrHandler
    {
        public const string ErrMsgStartService = "Start Local Host service on Sharp Odds maker and check is the same port number in Settings panel on both applications.";

        public static void ShowErrorMessage(string apiResponse, HttpStatusCode statusCode, Exception ex)
        {
            string errMessage;
            if (statusCode == HttpStatusCode.NotFound || statusCode == HttpStatusCode.Processing)
                errMessage = $"{ex.Message}.{Environment.NewLine}{Environment.NewLine}{ErrMsgStartService}";
            else
                errMessage = $"{statusCode.ToString().ToUpper()}. {apiResponse}. {ex.Message} ";

            MessageBox.Show(errMessage, statusCode.ToString().ToUpper());
        }
    }
}
