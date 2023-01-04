using Newtonsoft.Json;
using SharpApiTester.Api.Models;
using SharpApiTester.Api.Models.Import;
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
    public static class MakeOddsCaller
    {
        public async static Task<List<ExtendedMatchOddModel>> GetCalcOdds(int favorite, string winnerOdd, string drawOdd, string goals3p, int profitId, int calcType, string custSource = null)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/makeodds/calculate/{favorite}/{winnerOdd}/{drawOdd}/{goals3p}/{profitId}/{calcType}/{custSource}"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    CalcResultModel calcResultModel = JsonConvert.DeserializeObject<CalcResultModel>(apiResponse);

                    if (!string.IsNullOrEmpty(calcResultModel.Error))
                        MessageBox.Show(calcResultModel.Error, "Calculation Error");

                    if (!string.IsNullOrEmpty(calcResultModel.Warning))
                        MessageBox.Show(calcResultModel.Warning, "Calculation Warning");

                    List<ExtendedMatchOddModel> calcMatchOdds = calcResultModel.Odds;
                    return calcMatchOdds;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }


        public static async Task<string> LoadCalcMatchOddsToSharp(CalcOddRootModel calcOddRoot)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.PostAsJsonAsync($"{ApiGlobal.ServiceAddress}/api/makeodds/loadOdds", calcOddRoot))
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

        public static async Task<List<ProfitNameModel>> GetProfitNames()
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/makeodds/getProfitNames"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<List<ProfitNameModel>>(apiResponse);
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
