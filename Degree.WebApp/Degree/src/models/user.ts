export interface User {
    name : string;
    screenName: string;
    profileImage: string;
    statuses: number;
    retweets: number;
    favorites: number;
}

export enum UserList {
    Active,
    Retweet,
    Like
  }