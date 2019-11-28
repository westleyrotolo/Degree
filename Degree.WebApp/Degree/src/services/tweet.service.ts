import { Injectable } from '@angular/core';
import { BaseHttpService } from './base-http.service';
import { Observable } from 'rxjs';
import { Tweet } from 'src/models/tweet';
import { ApiRequest } from 'src/models/apiRequest';

@Injectable({
  providedIn: 'root'
})
export class TweetService {
  readonly BASE_URL = 'https://localhost:5001/TweetApi';
  constructor(private apiCLient: BaseHttpService) { }

  fetchTweets(apiRequest: ApiRequest):Observable<Tweet[]>{
      const url = '/Search';
      return this.apiCLient.post(apiRequest, this.BASE_URL + url);
  }
}
