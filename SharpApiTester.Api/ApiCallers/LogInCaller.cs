using SharpApiTester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpApiTester.Api.ApiCallers
{
    public static class LogInCaller
    {
        public async static Task LogIn(string email, string password)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.PostAsync($"{ApiGlobal.ServiceAddress}/api/auth/login", JsonContent.Create<LoginModel>(new LoginModel() { Email = email, Password = password })))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show(apiResponse);

                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
            }
        }

        
    }
}
