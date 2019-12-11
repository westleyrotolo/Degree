import { Injectable } from '@angular/core';
import { BaseHttpService } from './base-http.service';
import { Observable } from 'rxjs';
import { Tweet, HashtagsCount } from 'src/models/tweet';
import { ApiRequest } from 'src/models/apiRequest';
import { User } from 'src/models/user';
import { WordCloud } from 'src/models/wordCloud';
import { WordSentiment } from 'src/models/wordSentiment';
import { AvgHashtagSentiment } from 'src/models/avgHashtagSentiment';

@Injectable({
  providedIn: 'root'
})
export class TweetService {
 // readonly BASE_URL = 'https://degree-webapi.azurewebsites.net/TweetApi';
  readonly BASE_URL = 'https://localhost:5001/TweetApi';
  constructor(private apiClient: BaseHttpService) { }

  fetchTweets(apiRequest: ApiRequest):Observable<Tweet[]>{
      const url = '/Search';
      return this.apiClient.post(apiRequest, this.BASE_URL + url);
  }
  fetchHashtags():Observable<HashtagsCount[]>{
    const url = '/GetHashtags';
    return this.apiClient.getData(this.BASE_URL + url);
  }
  fetchUserMoreActives(take:number = 10, skip:number=0): Observable<User[]> {
    const url =`/MoreActives?take=${take}&skip=${skip}`;
    return this.apiClient.getData(this.BASE_URL + url);
  } 
  fetchUserMoreRetweets(take:number = 10, skip:number=0): Observable<User[]> {
    const url = `/MoreRetweets?take=${take}&skip=${skip}`;
    return this.apiClient.getData(this.BASE_URL+url);
  }
  fetchUserMoreFavorites(take:number =10, skip:number=0): Observable<User[]> {
    const url = `/MoreFavorites?take=${take}&skip=${skip}`;
    return this.apiClient.getData(this.BASE_URL+url);
  }
  fetchTagCloud(): Observable<WordCloud[]> {
    const url ='/TagCloud';
    return this.apiClient.getData(this.BASE_URL+url);
  }
  fetchWordSentiment(word:string): Observable<WordSentiment> {
    const url=`/AvgWord?word=${word}`;
    return this.apiClient.getData(this.BASE_URL+url);
  }
  fetchAvgSentiments(): Observable<AvgHashtagSentiment[]> {
    const url = '/AvgSentiments';
    return this.apiClient.getData(this.BASE_URL+url);
  }
}
