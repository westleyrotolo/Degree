import { Component, OnInit } from '@angular/core';
import { TweetService } from 'src/services/tweet.service';
import { Tweet } from 'src/models/tweet';
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
    transitionDuration: '0.8s'
  };
  tweets: Tweet[];
  title = 'Degree';
  updateMasonryLayout: boolean;
  hashtags: ToggleHashtag[] = [
    { hashtag: "#Adozionigay", isActive: true },
    { hashtag: "#Prolgbt", isActive: true },
    { hashtag: "#Famigliatradizionale", isActive: true },
    { hashtag: "#cirinnà", isActive: true },
    { hashtag: "famigliaarcobaleno", isActive: true },
    { hashtag: "#Congressodellefamiglie", isActive: true },
    { hashtag: "#Congressomondialedellefamiglie", isActive: true },
    { hashtag: "#WCFVerona", isActive: true },
    { hashtag: "#NoWCFVerona", isActive: true },
    { hashtag: "#No194", isActive: true },
    { hashtag: "#noeutonasia", isActive: true },
    { hashtag: "#uteroinaffitto", isActive: true },
    { hashtag: "#NoDDLPillon", isActive: true },
    { hashtag: "#Pillon", isActive: true },
    { hashtag: "#Pilloff", isActive: true },
    { hashtag: "#Spadafora", isActive: true },
    { hashtag: "#Affidocondiviso", isActive: true },
    { hashtag: "#Affidoparitario", isActive: true }
  ];
  tweetRequest: ApiRequest =
    {
      itemPerPage: 50,
      page: 0,
      hashtags: []
    }
  constructor(private tweetService: TweetService,
    private spinner: Ng4LoadingSpinnerService) { }
  ngOnInit() {
    this.spinner.show();
    this.resetRequest();
    this.tweetService.fetchTweets(this.tweetRequest).subscribe((resp) => {
      this.spinner.hide();

      this.tweets = resp;
      console.log('resp:', resp);
      console.log('tweets:', this.tweets);
    },
      (error?) => console.log(error));
  }
  resetRequest() {
    this.tweetRequest = {
      itemPerPage: 50,
      page: 0,
      hashtags: this.hashtags.filter(x=>x.isActive).map(x => x.hashtag )
    }
  }
  toggleHashtag(th: ToggleHashtag) {
    th.isActive = !th.isActive;
  }
  loadMore() {
    this.tweetRequest.page++;
    this.tweetRequest.hashtags = this.hashtags.filter(x=>x.isActive).map(x => x.hashtag )
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
