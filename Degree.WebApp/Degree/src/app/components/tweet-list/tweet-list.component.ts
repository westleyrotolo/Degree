import { Component, OnInit } from '@angular/core';
import { TweetService } from 'src/services/tweet.service';
import { Tweet, HashtagsCount } from 'src/models/tweet';
import { NgxMasonryOptions } from 'ngx-masonry';
import { ApiRequest, OrderSentiment } from 'src/models/apiRequest';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { MatSelect } from '@angular/material';
@Component({
  selector: 'app-tweet-list',
  templateUrl: './tweet-list.component.html',
  styleUrls: ['./tweet-list.component.scss']
})
export class TweetListComponent implements OnInit {
  public myOptions: NgxMasonryOptions = {
    transitionDuration: '0s',
    fitWidth: true
  };
  sentiments = [
    {value:  OrderSentiment.NoOrder, viewValue: 'Data Creazione'},
    {value: OrderSentiment.Positive, viewValue: 'Sentiment Positivo'},
    {value: OrderSentiment.Negative, viewValue: 'Sentiment Negativo'},
    {value:  OrderSentiment.Retweet, viewValue: 'Numero di Retweet'},
    {value: OrderSentiment.Favorite, viewValue: 'Numero di Favorite'},
    {value: OrderSentiment.Reply, viewValue: 'Numero di Reply'},
  ]; 
  defaultOrder = this.sentiments[0];
  selectedOrder = "Data Creazione";
  orderSentiments = [
    "Data Creazione",
    "Sentiment Positivo",
    "Sentiment Negativo",
    "Numero di Retweet",
    "Numero di Favorite", 
    "Numero di Reply"
  ];

  tweets: Tweet[];
  title = 'Degree';
  updateMasonryLayout: boolean;
  hashtags: HashtagsCount[] = [];
  tweetRequest: ApiRequest =
    {
      itemPerPage: 50,
      page: 0,
      hashtags: [],
      orderSentiment: OrderSentiment.NoOrder
    }
  toLoad = true
  constructor(private tweetService: TweetService,
    private spinner: Ng4LoadingSpinnerService) { }
  ngOnInit() {
    this.spinner.show();
    this.tweetService.fetchHashtags().subscribe((resp)=> {
      resp.forEach((x) => this.hashtags.push({
        isActive: true,
        count: x.count,
        hashtags: x.hashtags
      }));
      this.resetRequest();
      this.initTweetRequet();
    })

  }
  initTweetRequet(){
    this.spinner.show();
    this.resetRequest();
    this.tweetService.fetchTweets(this.tweetRequest).subscribe((resp) => {
      this.spinner.hide();
      this.toLoad = false;
      this.tweets = resp;
      console.log('resp:', resp);
      console.log('tweets:', this.tweets);
      this.spinner.hide();
    },
      (error?) => console.log(error)
    );
  }

  resetRequest() {
    this.tweetRequest = {
      itemPerPage: 50,
      page: 0,
      hashtags: this.hashtags.filter(x=>x.isActive).map(x => x.hashtags ),
      orderSentiment: this.sentiments.filter(x=>x.viewValue==this.selectedOrder)[0].value,
    }
  }
  toggleHashtag(th: HashtagsCount) {
    th.isActive = !th.isActive;
  }
  orderChanged($event) {
    console.log(this.selectedOrder);
  }
  loadMore() {
    this.myOptions.transitionDuration = '0.8s'
    this.tweetRequest.page++;
    this.spinner.show();
    this.tweetService.fetchTweets(this.tweetRequest).subscribe((resp) => {
      this.spinner.hide();

      if (resp != null) {
        resp.forEach(t => {
          this.tweets.push(t);

        });
        this.updateMasonryLayout = true;
      }
    },
      (error?) => console.log(error));
  }
}
export interface ToggleHashtag {
  hashtag: string,
  isActive: boolean
}