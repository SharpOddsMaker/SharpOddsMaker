using Newtonsoft.Json;
using SharpApiTester.Api;
using SharpApiTester.Api.Models;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpApiTester.Api.ApiCallers
{
    public static class DeleteOddsCaller
    {
        public static async Task DeleteBookmakerOdds(int bookmakerId)
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/deleteOdds/removeBookmaker/{bookmakerId}"))
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


        public async static Task<string> RemoveAll()
        {
            string apiResponse = "";
            HttpStatusCode statusCode = HttpStatusCode.Processing;

            HttpClient client = new HttpClient();

            try
            {
                using (var response = await client.GetAsync($"{ApiGlobal.ServiceAddress}/api/deleteOdds/clear"))
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
