import { Component, OnInit } from '@angular/core';
import { TweetService } from 'src/services/tweet.service';
import { Tweet } from 'src/models/tweet';
import { NgxMasonryOptions } from 'ngx-masonry';
import { ApiRequest } from 'src/models/apiRequest';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  public myOptions: NgxMasonryOptions = {
    transitionDuration: '0.8s'
  };
  tweets: Tweet[];
  title = 'Degree';
  constructor(private tweetService: TweetService) {}
  ngOnInit(){
    let initRequest: ApiRequest =
    {
      itemPerPage: 50,
      page: 0,
      hashtags: ["#Pillon", "#PillOff"]
    } 
    this.tweetService.fetchTweets(initRequest).subscribe((resp)=> {
      this.tweets = resp;
      console.log('resp:', resp);
      console.log('tweets:', this.tweets);
    });
    
  }
}
