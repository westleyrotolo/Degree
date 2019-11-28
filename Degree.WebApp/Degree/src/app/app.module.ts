import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BaseHttpService } from 'src/services/base-http.service';
import { TweetService } from 'src/services/tweet.service';
import { HttpClientModule } from '@angular/common/http';
import {
  MatCardModule, MatChipsModule, MatChipList
} from '@angular/material';
import { NgxMasonryModule } from 'ngx-masonry';
import { TweetCardComponent } from './components/tweet-card/tweet-card.component';
import { HashtagComponent } from './components/hashtag/hashtag.component';
import { NgCircleProgressModule } from 'ng-circle-progress';
import { PieSentimentComponent } from './components/pie-sentiment/pie-sentiment.component';
@NgModule({
  declarations: [
    AppComponent,
    TweetCardComponent,
    HashtagComponent,
    PieSentimentComponent
  ],
  entryComponents: [
    TweetCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatCardModule,
    MatChipsModule,
    NgxMasonryModule,
    NgCircleProgressModule.forRoot({
      radius: 30,
      outerStrokeWidth: 16,
      innerStrokeWidth: 8,
      animationDuration: 50,
    })
  ],  
  providers: [BaseHttpService, TweetService, HttpClientModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
