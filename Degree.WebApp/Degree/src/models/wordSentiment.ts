export interface WordSentiment {
    word: string;
    avgPositiveScore: number;
    avgNeutralScore: number;
    positiveLabel: number;
    neutralLabel: number;
    negativeLabel: number;
    mixedLabel: number;
    tweets: number
}