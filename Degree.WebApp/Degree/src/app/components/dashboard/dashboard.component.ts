import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import * as d3 from 'd3';
import * as t from 'topojson';
import {timer} from 'rxjs/observable/timer';
import { Subscription, Observable, TimeInterval } from 'rxjs';
import { Tweet } from 'src/models/tweet';
import { ScrollToBottomDirective } from 'src/directives/scroll-to-bottom.directive';
import { TweetService } from 'src/services/tweet.service';
import { WordCloud } from 'src/models/wordCloud';
import { GeoTweets, GroupedGeoTweets } from 'src/models/geoTweets';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy, AfterViewInit {


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

  displayedColumns = ['item'];
  @ViewChild('map', { static: false })
  mapDiv?: ElementRef<HTMLElement>;;
  @ViewChild('tweetsList', { static: false })
  tweetsList?: ElementRef<HTMLElement>;;
  tweets: Array<Tweet> = new Array<Tweet>();
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private tweetService: TweetService) {

  } 
  geoTweets: GroupedGeoTweets[] ;
  dates: Date[]
  updateMap() {

    return;
    this.tweetService.fetchGeoUsers().subscribe((x)=>
    {
        this.geoTweets = x;
        let index = 0;
       this.dates = this.geoTweets.map(x=>x.fromDate);
        this.dates = this.dates.filter( (thing, i, arr) => arr.findIndex(t => t === thing) === i);
        this.dates = this.dates.sort((one, two) => (one > two ? -1 : 1));
     

    });
  }
  /*
  updateDate( index = 0) {
    return;
    setTimeout(() => {
      this.startAnimation(this.dates[index]);
      index++
      this.updateDate(index);
    }, 1000);

  }
  */
  updateDate( date: Date, _this ) {
    let minDate = new Date(date.getTime() - (1000*60*30));
    timer(0, 100).subscribe(() => {
      
      console.log('start');
    console.log(date);
       let _tweets = _this.tweets.filter(x=>new Date(x.createdAt) <= date && new Date(x.createdAt) > minDate ).map(t=>  ({lat:t.geoCoordinate.lat, lon: t.geoCoordinate.lon, color: _this.getColor(t.sentiment)}));
       console.log(_tweets); 
       _tweets.forEach(element => {
         _this.addPin([element]);
       });
       date = new Date(date.getTime()+ (1000 * 60*30));
       minDate = new Date(date.getTime() - (1000*60*30));

   });


}

delay(ms: number) {
  return new Promise( resolve => setTimeout(resolve, ms) );
}

  startAnimation(fromDate: Date) {
    return;
    let index = 0;

    console.log(fromDate);
    let fDate = new Date(fromDate)
    let tweetsgeo = this.geoTweets.filter(x=> x.fromDate == fromDate).slice(0,30);
    console.log('tweetsgeo', tweetsgeo)
    tweetsgeo[0].tweets.forEach((element)=> {
    var coord: [{ lat: number, lon: number }  ] = [{ lat: element.latitude, lon: element.longitude }];
 //   this.addPin(coord, this.getColor(element.sentiment));

    })
  }
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.innerHeight;
  }

  goToWCF() {
    this.router.navigate(["/wcf-tweets"])
  }
  wordData: WordCloud[] = [];
  maxCount: number = 0;
  height = 600;
  coord: [{ lat, lon, color }] = [{ lat: 40.35, lon: 14.9833333, color: this.getColor("Positive") }]
  ngAfterViewInit() {
    const _this = this;
    console.log('test:', this.mapDiv);
    
      this.tweetService.fetchTagCloud().subscribe((resp: WordCloud[])=>{
        return;
        _this.wordData = resp;
        _this.maxCount = _this.wordData [0].count;
         console.log('data',_this.wordData)
      })
      this.italyMap();
      
  }
  loadPin() {
    this.tweetService.fetchGeoWCF().subscribe((resp: Tweet[]) => {
      console.log('Ok');
      
      this.tweets = resp.sort((a,b) =>  this.compareDate(a.createdAt, b.createdAt))
      console.log(this.tweets);
      let date = new Date(2019,2,29,7,0,0,0)
      this.updateDate(date, this);
    })
  }
 compareDate(date1: Date, date2: Date): number {
  // With Date object we can compare dates them using the >, <, <= or >=.
  // The ==, !=, ===, and !== operators require to use date.getTime(),
  // so we need to create a new instance of Date with 'new Date()'
  let d1 = new Date(date1); let d2 = new Date(date2);

  // Check if the dates are equal
  let same = d1.getTime() === d2.getTime();
  if (same) return 0;

  // Check if the first is greater than second
  if (d1 > d2) return 1;
 
  // Check if the first is less than second
  if (d1 < d2) return -1;
}
  
  public innerWidth: any;
  public innerHeight: any;
  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.innerHeight;

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


      }).then(() => this.addPin(this.coord));

  }
  italyMap() {
    var width = 500, height = 500;
    let svg = d3.select('div#map')
      .append('svg')
      .attr("width", width)
      .attr("height", height);
    d3.json("https://gist.githubusercontent.com/westleyrotolo/72cc18a7a09d178f66a8a24ffc9aca4a/raw/362bc521c1fd41bfccb7ef45940ed6bb224790e9/italy.json").then(function (it) {
      var projection = d3.geoAlbers()
        .center([0, 41])
        .rotate([347, 0])
        .parallels([35, 45])
        .scale(2000)
        .translate([width / 2, height / 2]);
      var subunits = t.feature(it, it.objects.sub);

      var path = d3.geoPath()
        .projection(projection);

      // draw border with sea
      svg.append("path")
        .datum(t.mesh(it, it.objects.sub, function (a, b) { return a === b; }))
        .attr("class", "border_map")
        .attr("d", path);
      // draw all the features together (no different styles)
      svg.append("path")
        .datum(subunits)
        .attr("class", "map")
        .attr("d", path);



      // draw and style any feature at time
      /*svg.selectAll("path")
      .data(topojson.feature(it, it.objects.sub).features)
      .enter().append("path")
      .attr("class",function(d) { return d.id; })
      .attr("d",path);*/

    }).then((x)=>  this.loadPin());
  } 
  
  addPin(coord: { lat: number, lon: number, color: string }[]) {
    var width = 500, height = 500;
    console.log('coordinate: ', coord);
    var projection = d3.geoAlbers()
    .center([0, 41])
    .rotate([347, 0])
    .parallels([35, 45])
    .scale(2000)
    .translate([width / 2, height / 2]);
    this.svg = d3.select("svg")
    this.svg.append("circle")
      .data(coord)
      .attr("cx", function (d) { return projection([d.lon, d.lat])[0] })
      .attr("cy", function (d) { return projection([d.lon, d.lat])[1] })
      .attr("r", 10 )
      .attr("fill", function(d) {return d.color})
      .style("stroke", function(d) {return d.color})
      .style("stroke-opacity", 1)
      .style("fill-opacity", 1)
      .transition()
      .duration(500)
      .ease(Math.sqrt)
      .style("stroke-opacity", .2)
      .style("fill-opacity", .2)
      .attr("r", 3)
      .remove();
  }
  ngOnDestroy(): void {
  }

}
//.style("stroke", d3.hsl((i = (i + 1) % 360), 1, .5))
