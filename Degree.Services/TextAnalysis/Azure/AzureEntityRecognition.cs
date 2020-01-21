using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Degree.Models;
using Degree.Models.Twitter;
using Degree.Services.TextAnalysis.Azure.Models;
using Degree.Services.TextAnalysis.Azure.Models.V3;
using Newtonsoft.Json;

namespace Degree.Services.TextAnalysis.Azure
{
    public class AzureEntityRecognition
    {
        public static async Task<EntityResponse_V3> EntityRecognitionV3PreviewPredictAsync(TextAnalyticsBatchInput inputDocuments)
        {
            Keys.LoadKey();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Keys.Azure.TEXT_ANALYSIS_KEY);

                var json = JsonConvert.SerializeObject(inputDocuments);


                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var httpResponse = await httpClient.PostAsync(new Uri(Keys.Azure.TEXT_ANALYSIS_ENTITY_URL), httpContent);
                var responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.StatusCode.Equals(HttpStatusCode.OK) || httpResponse.Content == null)
                {
                    throw new Exception(responseContent);
                }

                return JsonConvert.DeserializeObject<EntityResponse_V3>(responseContent, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
        }
    }
}
