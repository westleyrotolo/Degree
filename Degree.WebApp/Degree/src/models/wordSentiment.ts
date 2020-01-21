export interface WordSentiment {
    word: string;
    avgPositiveScore: number;
    avgNeutralScore: number;
    avgNegativeScore: number;
    positiveLabel: number;
    neutralLabel: number;
    negativeLabel: number;
    mixedLabel: number;
    tweets: number
}