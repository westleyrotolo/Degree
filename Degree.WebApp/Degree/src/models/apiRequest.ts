export interface ApiRequest {
    page: number;
    itemPerPage: number;
    hashtags: string[];
    orderSentiment: number;
}
export enum OrderSentiment {
    NoOrder,
    Positive,
    Negative,
    Retweet,
    Favorite,
    Reply
}