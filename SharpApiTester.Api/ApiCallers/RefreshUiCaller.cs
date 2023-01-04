using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.ApiCallers
{
    public static class RefreshUiCaller
    {
        public static async Task<string> RefreshUi()
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/refreshui"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }
    }
}
