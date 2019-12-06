import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import * as d3 from 'd3';
import * as t from 'topojson';
import { SignalRService } from 'src/services/signal-r.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {

  private signalRSubscription: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient, 
    private signalrService: SignalRService ) {

  }
  goToWCF() {
    this.router.navigate(["/wcf-tweets"])
  }
  coord:[{lat, lon}] = [{ lat: 40.35, lon: 14.9833333 }]

  ngOnInit() {
    this.signalRSubscription = this.signalrService.getMessage().subscribe(
      (message) => {
        console.log(message);
    });
    let width = 900;
    let height = 600;
    let projection = d3.geoMercator();

    let svg = d3.select('div#map')
      .append('svg')
      .attr('width', width)
      .attr('height', height);
    let path = d3.geoPath()
      .projection(projection);
    d3.json("https://gist.githubusercontent.com/westleyrotolo/22831144e03fcf0229d0ff478016ec7a/raw/401bb7aaadeea65d0cc11b9270cfe74138a68305/world.json")
      .then(function (topology) {
        svg.append('g')
          .attr('class', 'countries')
          .selectAll('path')
          .data(t.feature(topology, topology.objects.countries1).features)
          .enter()
          .append('path')
          .attr('d', path)
        svg.append('path')
          .attr("class", "countries-borders");

   
      }).then(()=>this.addPin(svg));
  }
  addPin(svg: any) {
    var projection = d3.geoMercator ()
    svg = d3.select("svg")
    svg.append("circle")
    .data(this.coord)
    .attr("cx", function(d) {return projection([d.lon, d.lat])[0]})
    .attr("cy", function(d) {return projection([d.lon, d.lat])[1]})
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
    this.signalrService.disconnect();
    this.signalRSubscription.unsubscribe();
  }

}
//.style("stroke", d3.hsl((i = (i + 1) % 360), 1, .5))
