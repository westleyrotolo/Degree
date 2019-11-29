import { Component, OnInit } from '@angular/core';
import { TweetService } from 'src/services/tweet.service';
import { Tweet, HashtagsCount } from 'src/models/tweet';
import { NgxMasonryOptions } from 'ngx-masonry';
import { ApiRequest } from 'src/models/apiRequest';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public myOptions: NgxMasonryOptions = {
    transitionDuration: '0s',

  };
  tweets: Tweet[];
  title = 'Degree';
  updateMasonryLayout: boolean;
  hashtags: HashtagsCount[] = [];
  tweetRequest: ApiRequest =
    {
      itemPerPage: 50,
      page: 0,
      hashtags: []
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
      hashtags: this.hashtags.filter(x=>x.isActive).map(x => x.hashtags )
    }
  }
  toggleHashtag(th: HashtagsCount) {
    th.isActive = !th.isActive;
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
