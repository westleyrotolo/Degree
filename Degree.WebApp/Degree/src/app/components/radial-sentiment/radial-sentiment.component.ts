import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import * as d3 from 'd3';
import { Label } from 'ng2-charts';
import { ChartType, ChartDataSets, RadialChartOptions } from 'chart.js';
import { TweetService } from 'src/services/tweet.service';
import { AvgHashtagSentiment } from 'src/models/avgHashtagSentiment';
@Component({
  selector: 'app-radial-sentiment',
  templateUrl: './radial-sentiment.component.html',
  styleUrls: ['./radial-sentiment.component.scss']
})
export class RadialSentimentComponent implements OnInit, AfterViewInit {
  ngAfterViewInit(): void {

  }

  @ViewChild('chart', { static: true })
  chart: Chart;
  public radarChartOptions: RadialChartOptions = {
    responsive: true,

  };
  avgHashtagsSentiment: AvgHashtagSentiment[];
  public radarChartLabels: Label[] = ['Eating', 'Drinking', 'Sleeping', 'Designing', 'Coding', 'Cycling', 'Running'];
  startAnimation(index: number) {
    this.radarChartLabels = this.avgHashtagsSentiment.map(x => x.hashtags);
    this.avgHashtagsSentiment.forEach(element => {
      if (index == 0) {
        this.radarChartData[0].data.push(element.avgSentiments[0].avgPositiveScore);
        this.radarChartData[0].data.push(element.avgSentiments[1].avgNeutralScore);
        this.radarChartData[0].data.push(element.avgSentiments[2].avgNegativeScore);
      } else {
        this.radarChartData[0].data[index] = element.avgSentiments[0].avgPositiveScore;
        this.radarChartData[0].data[index] = element.avgSentiments[1].avgNeutralScore;
        this.radarChartData[0].data[index] = element.avgSentiments[2].avgNegativeScore;
      }
      this.radarChartData = this.radarChartData.slice();
    });
  }
  updateData() {
    let index: number = 0
    this.startAnimation(index);

  }
  public radarChartData: ChartDataSets[] = [
    { data: [], label: 'Positive' },
    { data: [], label: 'Neutral' },
    { data: [], label: 'Negative' }
  ];
  public radarChartType: ChartType = 'radar';

  constructor(private tweetService: TweetService) { }

  ngOnInit() {
    this.tweetService.fetchAvgSentiments().subscribe((resp) => {
      console.log(resp);
      this.avgHashtagsSentiment = resp;
      this.updateData();
    })

  }


}
