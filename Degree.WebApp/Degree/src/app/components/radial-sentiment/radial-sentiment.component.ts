import { Component, OnInit, ViewChild, AfterViewInit, ɵConsole } from '@angular/core';
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
  currentDate: Date;
  public radarChartOptions: RadialChartOptions = {
    responsive: true,
    scale: {
      ticks: {
        min: 0,
        max: 1
      }
    }

  };
  avgHashtagsSentiment: AvgHashtagSentiment[];
  public radarChartLabels: Label[] = [];
  startAnimation(index: number) {
    this.avgHashtagsSentiment.forEach((element, i) => {
      if (index == 0) {
        this.radarChartData[0].data.push(element.avgSentiments[index].cumAvgPositiveScore);
        this.radarChartData[1].data.push(element.avgSentiments[index].cumAvgNegativeScore);
        this.radarChartData[2].data.push(element.avgSentiments[index].cumAvgNeutralScore);
        this.radarChartData2[0].data.push(element.avgSentiments[index].avgPositiveScore);
        this.radarChartData2[1].data.push(element.avgSentiments[index].avgNegativeScore);
        this.radarChartData2[2].data.push(element.avgSentiments[index].avgNeutralScore);
        let _element = element;
        

      } else {
        this.radarChartData[0].data[i] = element.avgSentiments[index].cumAvgPositiveScore
        this.radarChartData[1].data[i] = element.avgSentiments[index].cumAvgNegativeScore
        this.radarChartData[2].data[i] = element.avgSentiments[index].cumAvgNeutralScore
        this.radarChartData2[0].data[i] = element.avgSentiments[index].avgPositiveScore
        this.radarChartData2[1].data[i] = element.avgSentiments[index].avgNegativeScore
        this.radarChartData2[2].data[i] = element.avgSentiments[index].avgNeutralScore
       
      }

      console.log(this.radarChartData);
      this.radarChartData = [...this.radarChartData];
      this.radarChartData2 = [...this.radarChartData2];
    });
  }

  updateData(index: number = 0) {


    setTimeout(() => {

      console.log(index);
      this.startAnimation(index);
      index++
      if (index < this.avgHashtagsSentiment[0].avgSentiments.length-1)
        this.currentDate = this.avgHashtagsSentiment[0].avgSentiments[index].fromDate;
        this.updateData(index);
    }, 250);
  }


  public radarChartData: ChartDataSets[] = [
    { data: [], label: 'Positive', backgroundColor: 'rgba(0, 184, 148,0.6)', borderColor:'rgba(0, 184, 148,0.6)', pointBackgroundColor:'rgba(0, 184, 148,0.8)' , pointBorderColor:'rgba(255, 255, 255,1)'  },
    { data: [], label: 'Negative', backgroundColor: 'rgba(225, 112, 85,0.6)',  borderColor:'rgba(225, 112, 85,0.6)',  pointBackgroundColor:'rgba(225, 112, 85,0.8)' , pointBorderColor:'rgba(255, 255, 255,1)'},
    { data: [], label: 'Neutral', backgroundColor: 'rgba(116, 185, 255,0.6)', borderColor:'rgba(116, 185, 255,0.6)',  pointBackgroundColor:'rgba(116, 185, 255,0.8)' , pointBorderColor:'rgba(255, 255, 255,1)' }
  ];
  public radarChartData2: ChartDataSets[] = [
    { data: [], label: 'Positive', backgroundColor: 'rgba(0, 184, 148,0.6)', borderColor:'rgba(0, 184, 148,0.6)', pointBackgroundColor:'rgba(0, 184, 148,0.8)' , pointBorderColor:'rgba(255, 255, 255,1)'  },
    { data: [], label: 'Negative', backgroundColor: 'rgba(225, 112, 85,0.6)',  borderColor:'rgba(225, 112, 85,0.6)',  pointBackgroundColor:'rgba(225, 112, 85,0.8)' , pointBorderColor:'rgba(255, 255, 255,1)'},
    { data: [], label: 'Neutral', backgroundColor: 'rgba(116, 185, 255,0.6)', borderColor:'rgba(116, 185, 255,0.6)',  pointBackgroundColor:'rgba(116, 185, 255,0.8)' , pointBorderColor:'rgba(255, 255, 255,1)' }
   ];
  public radarChartType: ChartType = 'radar';

  constructor(private tweetService: TweetService) { }

  ngOnInit() {
    this.tweetService.fetchAvgSentiments().subscribe((resp) => {
      console.log('fetched', resp);
      this.avgHashtagsSentiment = resp;
      this.radarChartLabels = this.avgHashtagsSentiment.map(x => x.hashtags);
      console.log('count', this.avgHashtagsSentiment[0].avgSentiments.length);
      this.updateData();
    })

  }
  private delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

}
