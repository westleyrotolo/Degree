import { Component, OnInit, ElementRef, ViewChild, OnDestroy, AfterViewInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Tweet } from 'src/models/tweet';
import { SignalRService } from 'src/services/signal-r.service';
import { TweetService } from 'src/services/tweet.service';
import * as d3 from 'd3';
import * as t from 'topojson';
@Component({
  selector: 'app-experimental',
  templateUrl: './experimental.component.html',
  styleUrls: ['./experimental.component.scss']
})
export class ExperimentalComponent implements OnInit, OnDestroy, AfterViewInit {


  @ViewChild('map', { static: false })
  mapDiv?: ElementRef<HTMLElement>;
  private signalRSubscription: Subscription;
  public innerWidth: any;
  public innerHeight: any;  
  tweets: Array<Tweet> = new Array<Tweet>();
  height = 600;
  coord: [{ lat, lon }] = [{ lat: 40.35, lon: 14.9833333 }]
  positiveColor="#00b894";
  negativeColor="#d63031";
  mixedColor="#0984e3";
  neutralColor="#dfe6e9"
  getColor(sentiment: string): string {
    if (sentiment.toLowerCase() == "positive") {
      return this.positiveColor;
    } else if (sentiment.toLowerCase() == "negative") {
      return this.negativeColor;
    } else if (sentiment.toLowerCase() == "mixed") {
      return this.mixedColor;
    } else  {
      return this.neutralColor;
    }
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
        this.tweets.unshift(tweet);
        if (tweet.geoCoordinate && tweet.geoCoordinate.lat && tweet.geoCoordinate.lon) {
          var coord: [{ lat: number, lon: number }] = [{ lat: tweet.geoCoordinate.lat, lon: tweet.geoCoordinate.lon }];
          this.addPin(coord, this.getColor(tweet.sentiment));
        }
      });
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
  addPin(coord: [{ lat: number, lon: number }], color:string) {
    var projection = d3.geoMercator()
    this.svg = d3.select("svg")
    this.svg.append("circle")
      .data(coord)
      .attr("cx", function (d) { return projection([d.lon, d.lat])[0] })
      .attr("cy", function (d) { return projection([d.lon, d.lat])[1] })
      .attr("r", 2)
      .attr("fill", color)
      .style("stroke", color)
      .style("stroke-opacity", 1)
      .style("fill-opacity", 1)
      .transition()
      .duration(1500)
      .ease(Math.sqrt)
      .style("stroke-opacity", 0)
      .style("fill-opacity", 0)
      .attr("r", 4)
      .remove();
  }
  ngOnDestroy(): void {
    this.signalrService.disconnect();
    this.signalRSubscription.unsubscribe();
  }
}
