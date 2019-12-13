import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import * as d3 from 'd3';
import * as t from 'topojson';
import { Subscription } from 'rxjs';
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
    this.tweetService.fetchGeoUsers().subscribe((x)=>
    {
        this.geoTweets = x;
        let index = 0;
       this.dates = this.geoTweets.map(x=>x.fromDate);
        this.dates = this.dates.filter( (thing, i, arr) => arr.findIndex(t => t === thing) === i);
        this.dates = this.dates.sort((one, two) => (one > two ? -1 : 1));
        this.updateDate();
     

    });
  }
  updateDate( index = 0) {
    setTimeout(() => {
      this.startAnimation(this.dates[index]);
      index++
      this.updateDate(index);
    }, 1000);

  }
  startAnimation(fromDate: Date) {
    let index = 0;

    console.log(fromDate);
    let fDate = new Date(fromDate)
    let tweetsgeo = this.geoTweets.filter(x=> x.fromDate == fromDate).slice(0,30);
    console.log('tweetsgeo', tweetsgeo)
    tweetsgeo[0].tweets.forEach((element)=> {
    var coord: [{ lat: number, lon: number }  ] = [{ lat: element.latitude, lon: element.longitude }];
    this.addPin(coord, this.getColor(element.sentiment));

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
  coord: [{ lat, lon }] = [{ lat: 40.35, lon: 14.9833333 }]
  ngAfterViewInit() {
    const _this = this;
    console.log('test:', this.mapDiv);
      this.tweetService.fetchTagCloud().subscribe((resp: WordCloud[])=>{
        _this.wordData = resp;
        _this.maxCount = _this.wordData [0].count;
         console.log('data',_this.wordData)
      })
      this.italyMap()
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


      }).then(() => this.addPin(this.coord,this.getColor('Positive')));

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

    }).then((x)=>this.updateMap());
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
  }

}
//.style("stroke", d3.hsl((i = (i + 1) % 360), 1, .5))
