using Newtonsoft.Json;
using SharpApiTester.Api.Models.Import;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace SharpApiTester.Api.ApiCallers
{
    public static class MatchCaller
    {
        public async static Task<List<string>> GetMatchSources()
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/match/matchsources"))
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


        //Get matches by calling the localhost service and set Sports and Matches properties in ApiGlobal
        public static async Task<List<MatchRowModel>> GetSharpMatches(string custSource = null)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/match/getall/1/{custSource}"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    var matchRowModels = JsonConvert.DeserializeObject<List<MatchRowModel>>(apiResponse);

                    return matchRowModels;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }


        public static async Task LoadMatchDetailsIntoSharp(string fixtureId)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/match/loadMatchDetails/{fixtureId}"))
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

        public static async Task LoadMatchOddsIntoSharp(string fixtureId)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/match/loadMatchOdds/{fixtureId}/{ApiGlobal.ParamOddsOnly}"))
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
