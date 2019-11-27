using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Degree.Services.TextAnalysis.Azure.Models;
using Newtonsoft.Json;

namespace Degree.Services.TextAnalysis.Azure
{
    public static class AzureSentiment
    {


        public static async Task<SentimentResponse> SentimentV3PreviewPredictAsync(TextAnalyticsBatchInput inputDocuments)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Keys.Azure.TEXT_ANALYSIS_KEY);

                var httpContent = new StringContent(JsonConvert.SerializeObject(inputDocuments), Encoding.UTF8, "application/json");

                var httpResponse = await httpClient.PostAsync(new Uri(Keys.Azure.TEXT_ANALYSIS_URL), httpContent);
                var responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.StatusCode.Equals(HttpStatusCode.OK) || httpResponse.Content == null)
                {
                    throw new Exception(responseContent);
                }

                return JsonConvert.DeserializeObject<SentimentResponse>(responseContent, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
        }
    }
}
