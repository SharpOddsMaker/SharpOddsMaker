using Newtonsoft.Json;
using SharpApiTester.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api.ApiCallers
{
    public static class BookmakerCaller
    {
        public static async Task<Dictionary<int, BookmakerModel>> GetBookmakers()
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/bookmaker/getall"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    var bookmakers = JsonConvert.DeserializeObject<Dictionary<int, BookmakerModel>>(apiResponse);

                    return bookmakers;
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
