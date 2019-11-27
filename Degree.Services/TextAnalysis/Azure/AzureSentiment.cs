using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Degree.Models;
using Degree.Services.TextAnalysis.Azure.Models;
using Newtonsoft.Json;
using System.Linq;

namespace Degree.Services.TextAnalysis.Azure
{
    public static class AzureSentiment
    {
        public static async Task<List<TweetSentiment>> AnalyzeTweetSentiment(List<TweetRaw> tweets)
        {
            var tweetsSentiment = new List<TweetSentiment>();
            int page = 0;
            int itemPerPage = 50;
            
            
            var temp = new List<TweetRaw>();
            temp.AddRange(tweets.Skip(itemPerPage*page++).Take(itemPerPage));
            while (temp.Count > 0)
            {
                var textBatchInput =new TextAnalyticsBatchInput();
                foreach (var t in temp)
                {
                    var textAnalysis = new TextAnalyticsInput
                    {
                        Id = t.Id.ToString(),
                        Text = t.Text
                    };
                    textBatchInput.Documents.Add(textAnalysis);
                }
                var sentimentResponse = await SentimentV3PreviewPredictAsync(textBatchInput) ;
                Console.WriteLine($"Tweets analyzed:{itemPerPage + itemPerPage * (page-1)}");
                foreach (var document in sentimentResponse.Documents)
                {
                    var tweetSentiment = new TweetSentiment
                    {
                        TweetRawId = long.Parse(document.Id),
                        PositiveScore = document.DocumentScores.Positive,
                        NeutralScore = document.DocumentScores.Neutral,
                        NegativeScore = document.DocumentScores.Negative,
                        Sentiment = Enum.Parse<Degree.Models.DocumentSentimentLabel>(document.Sentiment.ToString()),
                    };
                    if (document.Sentences != null && document.Sentences.Count() > 0)
                    {
                        foreach (var sentence in document.Sentences)
                        {
                            var s = new TweetSentenceSentiment()
                            {
                                TweetSentimentId = long.Parse(document.Id),
                                Length = sentence.Length,
                                PositiveScore = sentence.SentenceScores.Positive,
                                NeutralScore = sentence.SentenceScores.Neutral,
                                NegativeScore = sentence.SentenceScores.Negative,
                                Offset = sentence.Offset,
                                Sentiment = Enum.Parse<Degree.Models.SentenceSentimentLabel>(sentence.Sentiment.ToString()),
                                Warnings = sentence.Warnings
                            };
                            tweetSentiment.Sentences.Add(s);
                        }
                    }
                    tweetsSentiment.Add(tweetSentiment);
                }
                temp.Clear();
                temp.AddRange(tweets.Skip(itemPerPage * page++).Take(itemPerPage));
            }
            return tweetsSentiment;
        }

        public static async Task<SentimentResponse> SentimentV3PreviewPredictAsync(TextAnalyticsBatchInput inputDocuments)
        {
            Keys.LoadKey();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Keys.Azure.TEXT_ANALYSIS_KEY);

                var json = JsonConvert.SerializeObject(inputDocuments);
                    
                    
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

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
