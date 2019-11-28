import { Component, OnInit, Input } from '@angular/core';
import { Tweet } from 'src/models/tweet';

@Component({
  selector: 'app-tweet-card',
  templateUrl: './tweet-card.component.html',
  styleUrls: ['./tweet-card.component.scss']
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
