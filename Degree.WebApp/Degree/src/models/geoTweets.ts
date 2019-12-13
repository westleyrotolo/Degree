export interface GeoTweets {
fromDate : Date
createdAt: Date
latitude : number
longitude:number
text:number
positiveScore:number
negativeScore:number
neutralScore: number
userScreenName:number
userProfileName:number    
sentiment: string     
}
export interface GroupedGeoTweets {
    fromDate: Date;
    tweets: GeoTweets[];
}