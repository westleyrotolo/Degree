export interface Tweet {
    id: number
    createdAt: Date
    text: string
    inReplyToStatusId?: number
    inReplyToScreenName?: string
    isRetweetStatus: boolean
    isQuoteStatus: boolean
    quoteCount: number
    replyCount: number
    retweetCount: number
    favoriteCount: number
    userId: number
    userName: string
    userScreenName: string
    userVerified: boolean
    userFollowers:number
    userFollowing: number
    userProfileImage: string
    userProfileBanner: string
    userDefaultProfile: boolean
    UserDefaultProfileImage: boolean
    sentiment: string
    positiveScore: number
    neutralScore:number
    negativeScore:number
    sentimentSentences?: SentimentSentence[]
    hashtags?: string[]
    EntityRecognizeds?: EntityRecognized[]
    geoCoordinate?: GeoCoordinate
}
export interface GeoCoordinate {
    lat: number
    lon: number
    geoName: string
}
export interface SentimentSentence{
    sentiment:string;
    positiveScore: number;
    neutralScore:number
    negativeScore:number
    offset:number
    length:number
}
export enum Sentiment {
    Positive = 'Positive',
    Negative = 'Negative',
    Neutral = 'Neutral',
    Mixed = 'Mixed'
}
export interface HashtagsCount {
    hashtags: string;
    count: number;
    isActive: boolean;    
}
export interface EntityRecognized {
    entityName: string;
    entityType: string;
    offset: number;
    length: number;
}