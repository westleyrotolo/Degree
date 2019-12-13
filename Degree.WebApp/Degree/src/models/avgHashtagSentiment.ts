export interface AvgHashtagSentiment {
    hashtags: string;
    avgSentiments: AvgSentiment[];

}
export interface AvgSentiment {
    avgPositiveScore : number;
    avgNegativeScore: number;
    avgNeutralScore: number;
    positiveLabel: number;
    neutralLabel: number;
    mixedLabel: number;
    negativeLabel: number;
    tweets: number;
    fromDate: Date;
    toDate: Date;
    cumAvgPositiveScore:number;
    cumAvgNeutralScore: number;
    cumAvgNegativeScore: number;
    cumulativeTweets: number
} 