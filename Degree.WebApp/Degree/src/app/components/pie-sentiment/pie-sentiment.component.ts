import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-pie-sentiment',
  templateUrl: './pie-sentiment.component.html',
  styleUrls: ['./pie-sentiment.component.scss']
})
export class PieSentimentComponent implements OnInit {

  constructor() { }
  @Input()
  label: string;
  
  @Input()
  percent: number;
  normalizedPercent: number;

  SUBTITLECOLOR: string = '#A9A9A9';
  TITLECOLOR: string =  '#444444';

  positiveColor: ChartColor = {
    innerStrokeColor: '#3E774B',
    outerStrokeColor: '#D4EDDA', 
    subtitleColor: this.SUBTITLECOLOR,
    titleColor: this.TITLECOLOR
  }
  neutralColor: ChartColor = {
    innerStrokeColor: '#74787B',
    outerStrokeColor: '#DBDCDF', 
    subtitleColor: this.SUBTITLECOLOR,
    titleColor: this.TITLECOLOR
  }

  negativeColor: ChartColor = {
    innerStrokeColor: '#975057',
    outerStrokeColor: '#F8D7DA', 
    subtitleColor: this.SUBTITLECOLOR,
    titleColor: this.TITLECOLOR
  }
  mixedColor: ChartColor = {
    innerStrokeColor: '#689BA4',
    outerStrokeColor: '#D1ECF1', 
    subtitleColor: this.SUBTITLECOLOR,
    titleColor: this.TITLECOLOR
  }
  activeColor: ChartColor;

  ngOnInit() {
    this.normalizedPercent = +(this.percent * 100).toFixed();
    if (this.label === 'Positive') {
      this.activeColor = this.positiveColor;
    } else if (this.label === 'Negative') {
      this.activeColor = this.negativeColor; 
    } else if (this.label === 'Mixed') {
      this.activeColor = this.mixedColor;
    } else if (this.label === 'Neutral') {
      this.activeColor = this.mixedColor;
    }

  }

}
export interface ChartColor {
  innerStrokeColor: string;
  outerStrokeColor: string;
  subtitleColor: string;
  titleColor: string;

}
