import { Component, OnInit, Input } from '@angular/core';
import { Tweet } from 'src/models/tweet';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-tweet-card',
  templateUrl: './tweet-card.component.html',
  styleUrls: ['./tweet-card.component.scss'],
  animations: [
    trigger('openClose', [
      // ...
      state('open', style({
        height: '200px',
        opacity: 1,
      })),
      state('closed', style({
        height: '100px',
        opacity: 0.5,
      })),
      transition('* => closed', [
        animate('1s')
      ]),
      transition('* => open', [
        animate('2s')
      ]),
    ]),
  ],
})
export class TweetCardComponent implements OnInit {

  @Input()
  tweet: any;
  profilePic: string;
  sentimentClass: string;
  constructor() { }
 
  ngOnInit() {
    console.log("tweet: " ,this.tweet);
    this.profilePic = `url(${this.tweet.userProfileImage})`;
    console.log("profilePic: ", this.profilePic);
    this.SetSentimentClass();
  }

  SetSentimentClass() {
    if (this.tweet.sentiment === 'Positive') {
      this.sentimentClass = 'alert alert-success'
    } else if (this.tweet.sentiment === 'Negative') {
      this.sentimentClass = 'alert alert-danger';
    } else if (this.tweet.sentiment === 'Neutral') {
      this.sentimentClass = 'alert alert-secondary';
    } else if (this.tweet.sentiment === 'Mixed') {
      this.sentimentClass = 'alert alert-info';
    }
  }
}
