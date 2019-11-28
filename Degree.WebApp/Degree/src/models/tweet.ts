export interface Tweet {
    Id: number
    CreatedAt: Date
    Text: string
    InReplyToStatusId?: number
    InReplyToScreenName?: string
    IsRetweetStatus: boolean
    IsQuoteStatus: boolean
    QuoteCount: number
    ReplyCount: number
    RetweetCount: number
    FavoriteCount: number
    UserId: number
    UserName: string
    UserScreenName: string
    UserVerified: boolean
    UserFollowers:number
    UserFollowing: number
    UserProfileImage: string
    UserProfileBanner: string
    UserDefaultProfile: boolean
    UserDefaultProfileImage: boolean
    Sentiment: string
    PositiveScore: number
    NeutralScore:number
    NegativeScore:number
    SentimentSentences?: SentimentSentence[]
}
export interface SentimentSentence{
    Sentiment:string;
    PositiveScore: number;
    NeutralScore:number
    NegativeScore:number
    Offset:number
    Length:number
}
export enum Sentiment {
    Positive = 'Positive',
    Negative = 'Negative',
    Neutral = 'Neutral',
    Mixed = 'Mixed'
}