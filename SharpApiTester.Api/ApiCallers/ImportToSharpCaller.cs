using SharpApiTester.Api.Models.Export;
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
    public static class ImportToSharpCaller
    {
        public async static Task ImportMatches(List<CustomMatch> customMatches)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.PostAsJsonAsync($"{ApiGlobal.ServiceAddress}/api/import/matches", customMatches))
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


        public async static Task ImportOdds(CustomOddsRootModel customOddsRootModel)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.PostAsJsonAsync($"{ApiGlobal.ServiceAddress}/api/import/odds", customOddsRootModel))
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
