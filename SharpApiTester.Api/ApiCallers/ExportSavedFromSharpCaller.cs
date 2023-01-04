using Newtonsoft.Json;
using SharpApiTester.Api.Models;
using SharpApiTester.Api.Models.Export;
using SharpApiTester.Api.Models.Import;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpApiTester.Api.ApiCallers
{
    public static class ExportSavedFromSharpCaller
    {
        public async static Task<List<SaveSessionModel>> GetSessions()
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/export/getSessions"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    List<SaveSessionModel> savedSessions = JsonConvert.DeserializeObject<List<SaveSessionModel>>(apiResponse);
                    return savedSessions;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }


        public async static Task<List<MatchRowModel>> GetSavedMatches(int sessionId, string matchSource)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/export/matches/{sessionId}/{matchSource}"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    List<MatchRowModel> savedMatches = JsonConvert.DeserializeObject<List<MatchRowModel>>(apiResponse);

                    return savedMatches;
                }
            }
            catch (Exception ex)
            {
                CallerErrHandler.ShowErrorMessage(apiResponse, statusCode, ex);
                return null;
            }
        }


        public async static Task<List<MatchOddWithNamesModel>> GetOdds4SelectedMatch(int sessionId, int fixtureId, string oddsSource=null)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/export/odds/{sessionId}/{fixtureId}/{oddsSource}"))
                {
                    statusCode = response.StatusCode;
                    apiResponse = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    List<MatchOddWithNamesModel> savedOdds = JsonConvert.DeserializeObject<List<MatchOddWithNamesModel>>(apiResponse);
                    return savedOdds;
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
