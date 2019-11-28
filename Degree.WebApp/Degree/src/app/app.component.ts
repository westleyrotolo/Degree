import { Component, OnInit } from '@angular/core';
import { TweetService } from 'src/services/tweet.service';
import { Tweet } from 'src/models/tweet';
import { NgxMasonryOptions } from 'ngx-masonry';
import { ApiRequest } from 'src/models/apiRequest';
import { isNgContainer } from '@angular/compiler';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public myOptions: NgxMasonryOptions = {
    transitionDuration: '0.8s'
  };
  tweets: Tweet[];
  title = 'Degree';
  hashtags: ToggleHashtag[] = [
    { hashtag:"#Adozionigay", isActive: true},
    { hashtag:"#Prolgbt", isActive: true},
    { hashtag:"#Famigliatradizionale", isActive: true},
    { hashtag:"#cirinnà", isActive: true},
    { hashtag:"famigliaarcobaleno", isActive: true},
    { hashtag:"#Congressodellefamiglie", isActive: true},
    { hashtag:"#Congressomondialedellefamiglie", isActive: true},
    { hashtag:"#WCFVerona", isActive: true},
    { hashtag:"#NoWCFVerona", isActive: true},
    { hashtag:"#No194", isActive: true},
    { hashtag:"#noeutonasia", isActive: true},
    { hashtag:"#uteroinaffitto", isActive: true},
    { hashtag:"#NoDDLPillon", isActive: true},
    { hashtag:"#Pillon", isActive: true},
    { hashtag:"#Pilloff", isActive: true},
    { hashtag:"#Spadafora", isActive: true},
    { hashtag:"#Affidocondiviso", isActive: true},
    { hashtag:"#Affidoparitario", isActive: true}
  ];
  constructor(private tweetService: TweetService) { }
  ngOnInit() {
    let initRequest: ApiRequest =
    {
      itemPerPage: 50,
      page: 0,
      hashtags: ["#Pillon", "#PillOff"]
    }
    this.tweetService.fetchTweets(initRequest).subscribe((resp) => {
      this.tweets = resp;
      console.log('resp:', resp);
      console.log('tweets:', this.tweets);
    });
  }
  toggleHashtag(th: ToggleHashtag) {
    th.isActive = !th.isActive;
  }
}
export interface ToggleHashtag {
  hashtag: string,
  isActive: boolean
}
