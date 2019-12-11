import { Component, OnInit, Input } from '@angular/core';
import { WordSentiment } from 'src/models/wordSentiment';

@Component({
  selector: 'app-word-sentiment',
  templateUrl: './word-sentiment.component.html',
  styleUrls: ['./word-sentiment.component.scss']
})
export class WordSentimentComponent implements OnInit {

  @Input()
  wordSentiment: WordSentiment;
  constructor() { }

  ngOnInit() {
  }

}
