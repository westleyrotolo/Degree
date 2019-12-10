import { Component, OnInit } from '@angular/core';
import { OrderSentiment, ApiRequest } from 'src/models/apiRequest';
import { Tweet } from 'src/models/tweet';
import { TweetService } from 'src/services/tweet.service';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-tweet-list',
  templateUrl: './tweet-list.component.html',
  styleUrls: ['./tweet-list.component.scss']
})
export class TweetListComponent implements OnInit {

  hashtags: string[] = [
    "#AdozioniGay",
    "#ProLGBT",
    "#FamigliaTradizionale",
    "#Cirnnà",
    "#FamigliaArcobaleno",
    "#CongressoMondialeDelleFamiglie",
    "#CongressoDelleFamiglie",
    "#Cirinnà",
    "#WCFVerona",
    "#WCF",
    "#NoWCFVerona",
    "#No194",
    "#UteroInAffitto",
    "#NoDDlPillon",
    "#Pillon",
    "#PillOFF",
    "#AffidoCondiviso",
    "#AffidoParitario"
  ];
  selectedOrder = "Numero di Retweet";
  orderSentiments = [
    "Numero di Retweet",
    "Numero di Favorite",
    "Numero di Reply",
    "Sentiment Positivo",
    "Sentiment Negativo",
  ];

  tweets: Tweet[];
  tweetRequest: ApiRequest =
    {
      itemPerPage: 5,
      page: 0,
      hashtags: [] = this.hashtags,
      orderSentiment: OrderSentiment.Retweet
    }
  constructor(private tweetService: TweetService, private spinner: Ng4LoadingSpinnerService) { }

  orderChanged($event) {
    console.log(this.selectedOrder);
    this.spinner.show();
    if (this.selectedOrder == "Numero di Retweet") {
      this.tweetRequest.orderSentiment = OrderSentiment.Retweet;
    } else if (this.selectedOrder == "Numero di Favorite") {
      this.tweetRequest.orderSentiment = OrderSentiment.Favorite;
    } else if (this.selectedOrder == "Numero di Reply") {
      this.tweetRequest.orderSentiment = OrderSentiment.Reply;
    } else if (this.selectedOrder == "Sentiment Positivo") {
      this.tweetRequest.orderSentiment = OrderSentiment.Positive;
    } else if (this.selectedOrder == "Sentiment Negativo") {
      this.tweetRequest.orderSentiment ==OrderSentiment.Negative;
    }
    console.log(this.tweetRequest);
    this.tweetService.fetchTweets(this.tweetRequest).subscribe((resp) => {
      this.tweets = resp;
      this.spinner.hide();
    });
  }
  ngOnInit() {
    this.spinner.show()
    this.tweetService.fetchTweets(this.tweetRequest).subscribe((resp) => {
      this.tweets = resp;
      this.spinner.hide();
    });
  }


}
