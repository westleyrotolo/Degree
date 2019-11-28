import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

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
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
@NgModule({
  declarations: [
    AppComponent,
    TweetCardComponent,
    HashtagComponent,
    PieSentimentComponent,
  ],
  entryComponents: [
    TweetCardComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatCardModule,
    MatChipsModule,
    NgxMasonryModule,
    Ng4LoadingSpinnerModule.forRoot()
  ],  
  providers: [BaseHttpService, TweetService, HttpClientModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
