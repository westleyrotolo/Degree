using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Degree.Models;
using Degree.Models.Twitter;
using Degree.Services.TextAnalysis.Azure.Models;

namespace Degree.Services.TextAnalysis.Azure
{
    public static class TweetCognitive
    {
        public static async Task<List<TweetSentiment>> AnalyzeTweetSentiment(List<TweetRaw> tweets)
        {
            var tweetsSentiment = new List<TweetSentiment>();
            int page = 0;
            int itemPerPage = 50;


            var temp = new List<TweetRaw>();
            temp.AddRange(tweets.Skip(itemPerPage * page++).Take(itemPerPage));
            while (temp.Count > 0)
            {
                var textBatchInput = new TextAnalyticsBatchInput();
                foreach (var t in temp)
                {
                    var textAnalysis = new TextAnalyticsInput
                    {
                        Id = t.Id.ToString(),
                        Text = t.Text
                    };
                    textBatchInput.Documents.Add(textAnalysis);
                }
                var sentimentResponse = await AzureSentiment.SentimentV3PreviewPredictAsync(textBatchInput);
                Console.WriteLine($"Tweets analyzed:{itemPerPage + itemPerPage * (page - 1)}");
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

        public static async Task<List<TweetEntityRecognized>> AnalyzeTweetEntity(List<TweetRaw> tweets)
        {
            var tweetsEntity = new List<TweetEntityRecognized>();
            int page = 0;
            int itemPerPage = 50;


            var temp = new List<TweetRaw>();
            temp.AddRange(tweets.Skip(itemPerPage * page++).Take(itemPerPage));
            while (temp.Count > 0)
            {
                var textBatchInput = new TextAnalyticsBatchInput();
                foreach (var t in temp)
                {
                    var textAnalysis = new TextAnalyticsInput
                    {
                        Id = t.Id.ToString(),
                        Text = t.Text
                    };
                    textBatchInput.Documents.Add(textAnalysis);
                }
                var entityRecognitionResponse = await AzureEntityRecognition.EntityRecognitionV3PreviewPredictAsync(textBatchInput);
                Console.WriteLine($"Tweets analyzed:{itemPerPage + itemPerPage * (page - 1)}");
                foreach (var document in entityRecognitionResponse.Documents)
                {
                    foreach (var entity in document.Entities)
                    {
                        var tweetEntity = new TweetEntityRecognized
                        {
                            EntityName = entity.Text,
                            EntityType = entity.Type,
                            Length = entity.Length,
                            Offset = entity.Offset,
                            Score = entity.Score,
                            TweetRawId = long.Parse(document.Id)
                        };
                        tweetsEntity.Add(tweetEntity);
                    }
                }
                temp.Clear();
                temp.AddRange(tweets.Skip(itemPerPage * page++).Take(itemPerPage));
            }
            return tweetsEntity;
        }

    }
}
