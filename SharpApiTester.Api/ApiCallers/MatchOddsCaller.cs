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
    public static class MatchOddsCaller
    {
        public async static Task<List<string>> GetOddSources()
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/matchodds/oddsources"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    List<string> oddsSources = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                    return oddsSources;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }


        public async static Task<List<MatchOddWithNamesModel>> GetAllMatchOdds(int fixtureId, string custSource = null)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/matchodds/getMatchOdds/{fixtureId}/{ApiGlobal.ParamOddsOnly}/{custSource}"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    List<MatchOddWithNamesModel> matchOdds = JsonConvert.DeserializeObject<List<MatchOddWithNamesModel>>(apiResponse);
                    return matchOdds;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }


        public async static Task<List<MatchOddWithNamesModel>> GetNotCalcOdds(int fixtureId, string custSource = null)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/matchodds/getMissingMatchOdds/{fixtureId}/{custSource}"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    List<MatchOddWithNamesModel> matchOdds = JsonConvert.DeserializeObject<List<MatchOddWithNamesModel>>(apiResponse);
                    return matchOdds;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }


        public async static Task<List<OddNameMappedModel>> GetOddNames(string custSource=null)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/matchodds/oddnames/{custSource}"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    List<OddNameMappedModel> oddNames = JsonConvert.DeserializeObject<List<OddNameMappedModel>>(apiResponse);
                    return oddNames;
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
