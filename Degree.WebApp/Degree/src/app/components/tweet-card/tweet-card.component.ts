import { Component, OnInit, Input } from '@angular/core';
import { Tweet } from 'src/models/tweet';

@Component({
  selector: 'app-tweet-card',
  templateUrl: './tweet-card.component.html',
  styleUrls: ['./tweet-card.component.scss']
})
export class TweetCardComponent implements OnInit {

  @Input()
  tweet: Tweet;
  profilePic: string;
  constructor() { }
 
  ngOnInit() {
    console.log("tweet: " ,this.tweet);
    this.profilePic = `url(${this.tweet.UserProfileImage})`;
    console.log("profilePic: ", this.profilePic);
  }

}
