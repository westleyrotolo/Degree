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
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy, AfterViewInit {


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

    });
  }
  addPin(coord: [{ lat: number, lon: number }]) {
    var projection = d3.geoMercator()
    this.svg = d3.select("svg")
    this.svg.append("circle")
      .data(coord)
      .attr("cx", function (d) { return projection([d.lon, d.lat])[0] })
      .attr("cy", function (d) { return projection([d.lon, d.lat])[1] })
      .attr("r", 1)
      .attr("fill", "#ff0000")
      .style("stroke", "#ff0000")
      .style("stroke-opacity", 1)
      .style("fill-opacity", 1)
      .transition()
      .duration(1000)
      .ease(Math.sqrt)
      .style("stroke-opacity", 0)
      .style("fill-opacity", 0)
      .attr("r", 2)
      .remove();
  }
  ngOnDestroy(): void {
  }

}
//.style("stroke", d3.hsl((i = (i + 1) % 360), 1, .5))
