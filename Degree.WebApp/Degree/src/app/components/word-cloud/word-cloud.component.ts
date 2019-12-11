import { Component, Input, OnInit, OnChanges, Inject } from '@angular/core';
import * as d3 from 'd3';
import { WordCloud } from 'src/models/wordCloud';
import { TweetService } from 'src/services/tweet.service';
import * as cloud from 'd3-cloud'
import * as scale from 'd3-scale'
import { schemeCategory10 } from 'd3';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { WordSentiment } from 'src/models/wordSentiment';
@Component({
  selector: 'app-word-cloud',
  templateUrl: './word-cloud.component.html',
  styleUrls: ['./word-cloud.component.scss']
})
export class WordCloudComponent implements OnInit, OnChanges {
  MAX_SIZE = 100;
  @Input() wordData: WordCloud[] = [];
  @Input() maxCount: number;


  data = [];

  private svg;               // SVG in which we will print our chart
  private margin: {          // pace between the svg borders and the actual chart graphic
    top: number,
    right: number,
    bottom: number,
    left: number
  };
  private width: number;      // Component width
  private height: number;     // Component height
  private fillScale;          // D3 scale for text color
  tempData = [];

  constructor(private tweetService: TweetService,private dialog: MatDialog) {
  }
  ngOnChanges() {
    this.draw();
  }

  ngOnInit() {
  }

  draw() {
    if (this.wordData.length == 0)
      return;
    let myWords: { word: string, size: number }[] = [];
    const _this = this;
    this.wordData.forEach((x)=>
    {
        let _size = x.count/this.maxCount*this.MAX_SIZE;
        const myWord:{word:string, size:number} = 
        {
          size: _size < 12 ? 12 : _size,
          word: x.word
        }
        myWords.push(myWord);
    })
    // set the dimensions and margins of the graph
    let margin = { top: 10, right: 10, bottom: 10, left: 10 },
      width = 450 - margin.left - margin.right,
      height = 450 - margin.top - margin.bottom;

    // append the svg object to the body of the page
    var svg = d3.select("#my_dataviz").append("svg")
      .attr("width", width + margin.left + margin.right)
      .attr("height", height + margin.top + margin.bottom)
      .append("g")
      .attr("transform",
        "translate(" + margin.left + "," + margin.top + ")");

    // Constructs a new cloud layout instance. It run an algorithm to find the position of words that suits your requirements
    // Wordcloud features that are different from one word to the other must be here
    var layout = cloud()
      .size([width, height])
      .words(myWords.map(function (d) { return { text: d.word, size: d.size }; }))
      .padding(5)        //space between words
      .rotate(function () { return ~~(Math.random() * 2) * 90; })
      .fontSize(function (d) { return d.size; })      // font size of words
      .on("end", draw);
    layout.start();
    // This function takes the output of 'layout' above and draw the words
    // Wordcloud features that are THE SAME from one word to the other can be here
    function draw(words) {
    var fill =  d3.schemeCategory10;
      svg
        .append("g")
        .attr("transform", "translate(" + layout.size()[0] / 2 + "," + layout.size()[1] / 2 + ")")
        .selectAll("text")
        .data(words)
        .enter().append("text")
        .on("click", function(d: any) {
          _this.tweetService.fetchWordSentiment(d.text).subscribe((resp)=> {
            const dialogRef = _this.dialog.open(DialogOverview, {
              width: '500px',
              data: {wordSentiment: resp}
          });
        })})
        .style("font-size", function (d: any) { return d.size; })
        .style("fill", function(d, i) { return fill[i] })
        .attr("text-anchor", "middle")
        .style("font-family", "Impact")
        .style("cursor","pointer")
        .attr("transform", function (d: any) {
          return "translate(" + [d.x, d.y] + ")rotate(" + d.rotate + ")";
        })
        .text(function (d: any) { return d.text; });
    }
  }
  
}
@Component({
  selector: 'word-cloud-dialog',
  templateUrl: 'word-cloud-dialog.html',
})
export class DialogOverview {

  constructor(
    public dialogRef: MatDialogRef<DialogOverview>,
    @Inject(MAT_DIALOG_DATA) public data: {wordSentiment: WordSentiment}) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}