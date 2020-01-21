import { transition } from '@angular/animations';
import { Component, OnInit, ElementRef, ViewChild, OnDestroy, AfterViewInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Tweet } from 'src/models/tweet';
import { SignalRService } from 'src/services/signal-r.service';
import { TweetService } from 'src/services/tweet.service';
import * as d3 from 'd3';
import * as t from 'topojson';
import { LayoutModule } from '@angular/cdk/layout';
import { QueryStream } from 'src/models/queryStream';
import { TweetSentiment } from 'src/models/tweetSentiment';
@Component({
  selector: 'app-experimental',
  templateUrl: './experimental.component.html',
  styleUrls: ['./experimental.component.scss']
})
export class ExperimentalComponent implements OnInit, OnDestroy, AfterViewInit {


  q: string = "";
  @ViewChild('map', { static: false })
  mapDiv?: ElementRef<HTMLElement>;
  private signalRSubscription: Subscription;
  public innerWidth: any;
  public innerHeight: any;
  queryStream: QueryStream = { geoEnabled: false, tracks: [] };
  tweets: Array<Tweet> = new Array<Tweet>();
  tweetSentiment: TweetSentiment = {avgNeutralScore: 0, avgPositiveScore: 0, avgNegativeScore: 0, mixedLabel:0, positiveLabel: 0, negativeLabel:0, neutralLabel:0, tweets:0};
  height = 600;
  coord: [{ lat, lon }] = [{ lat: 40.35, lon: 14.9833333 }]
  positiveColor = "#00b894";
  negativeColor = "#d63031";
  mixedColor = "#3c6382";
  neutralColor = "#3c6382"
  getColor(sentiment: string): string {
    if (sentiment.toLowerCase() == "positive") {
      return this.positiveColor;
    } else if (sentiment.toLowerCase() == "negative") {
      return this.negativeColor;
    } else if (sentiment.toLowerCase() == "mixed") {
      return this.mixedColor;
    } else {
      return this.mixedColor;
    }
  }
  updateTrack() {
    if (this.q !== '') {
      console.log('update')
      this.queryStream.tracks.push(this.q)
      console.log(this.queryStream);
      this.q = '';
    }
  }
  trackStream() {
    console.log('start')
    this.signalrService.sendMessage(this.queryStream);
    console.log('end')

  }
  updateNumber(currentValue: number, newValue: number ) {
    d3.select("#tweetnumber").transition().duration(500).delay(0)
    .tween("text", function(d) {
      var i = d3.interpolate(currentValue, newValue);
      return function(t) {
        d3.select(this).text((Math.ceil(i(t))));
      };
    });
  }

  constructor(
    private signalrService: SignalRService,
    private tweetService: TweetService) { }

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.innerHeight;
  }
  ngAfterViewInit() {
    const _this = this;
    console.log('test:', this.mapDiv);
    this.worldMap();
    this.signalRSubscription = this.signalrService.getMessage().subscribe(
      (tweet) => {
        console.log(tweet);
        this.updateNumber(this.tweets.length, this.tweets.length+1);
        this.tweets.unshift(tweet);
        this.updateSentiment();
        if (tweet.geoCoordinate && tweet.geoCoordinate.lat && tweet.geoCoordinate.lon) {
          var coord: [{ lat: number, lon: number }] = [{ lat: tweet.geoCoordinate.lat, lon: tweet.geoCoordinate.lon }];
          console.log('add pin', coord)

          this.addPin(coord, this.getColor(tweet.sentiment));
        }
      });
  }
  utils: Utils = new Utils();
  updateSentiment() {
    this.tweetSentiment.tweets = this.tweets.length;
    this.tweetSentiment.avgPositiveScore = (this.tweets.map(x=>x.positiveScore).reduce((sum, current) => sum + current, 0)) / this.tweets.length;
    this.tweetSentiment.avgNeutralScore = (this.tweets.map(x=>x.neutralScore).reduce((sum, current) => sum + current, 0)) / this.tweets.length;
    this.tweetSentiment.avgNegativeScore = (this.tweets.map(x=>x.negativeScore).reduce((sum, current) => sum + current, 0)) / this.tweets.length;
    this.tweetSentiment.positiveLabel = this.tweets.filter(x=>x.sentiment === 'Positive').length;
    this.tweetSentiment.neutralLabel = this.tweets.filter(x=>x.sentiment === 'Neutral').length;
    this.tweetSentiment.mixedLabel = this.tweets.filter(x=>x.sentiment === 'Mixed').length;
    this.tweetSentiment.negativeLabel = this.tweets.filter(x=>x.sentiment === 'Negative').length;

    console.log(this.tweetSentiment);
  }

  svg: any = {};
  worldMap() {
    let _width = this.innerWidth - 450;
    let _height = _width * 0.48;
    let width = _width;
    this.height = _height;
    let projection = d3.geoMercator();

    this.svg = d3.select('div#map')
      .append('svg')
      .attr('width', width)
      .attr('height', this.height);
    let _svg = this.svg;
    let path = d3.geoPath()
      .projection(projection);
    d3.json("https://gist.githubusercontent.com/westleyrotolo/22831144e03fcf0229d0ff478016ec7a/raw/401bb7aaadeea65d0cc11b9270cfe74138a68305/world.json")
      .then(function (topology) {
        _svg.append('g')
          .attr('class', 'countries')
          .selectAll('path')
          .data(t.feature(topology, topology.objects.countries1).features)
          .enter()
          .append('path')
          .attr('d', path)
        _svg.append('path')
          .attr("class", "countries-borders");


      });
  }
  addPin(coord: [{ lat: number, lon: number }], color: string) {
    console.log('coordinate: ', coord);
    var projection = d3.geoMercator()
    this.svg = d3.select("svg")
    this.svg.append("circle")
      .data(coord)
      .attr("cx", function (d) { return projection([d.lon, d.lat])[0] })
      .attr("cy", function (d) { return projection([d.lon, d.lat])[1] })
      .attr("r", 20)
      .attr("fill", color)
      .style("stroke", color)
      .style("stroke-opacity", 1)
      .style("fill-opacity", 1)
      .transition()
      .duration(600)
      .ease(Math.sqrt)
      .style("stroke-opacity", .5)
      .style("fill-opacity", .5)
      .attr("r", 5);
    //.remove();
  }
  ngOnDestroy(): void {
    this.signalrService.disconnect();
    this.signalRSubscription.unsubscribe();
  }
  removeTag(q: string) {
    this.queryStream.tracks = this.queryStream.tracks.filter(x => x != q);
  }
}


export class Utils {
  constructor() { }

  groupBy<T, K>(list: T[], getKey: (item: T) => K) {
      const map = new Map<K, T[]>();
      list.forEach((item) => {
          const key = getKey(item);
          const collection = map.get(key);
          if (!collection) {
              map.set(key, [item]);
          } else {
              collection.push(item);
          }
      });
      return Array.from(map.values());
  }
}