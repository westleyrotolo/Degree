import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BaseHttpService } from 'src/services/base-http.service';
import { TweetService } from 'src/services/tweet.service';
import { HttpClientModule } from '@angular/common/http';
import {
  MatCardModule, MatChipsModule, MatChipList, MatSelectModule
} from '@angular/material';
import { NgxMasonryModule } from 'ngx-masonry';
import { TweetCardComponent } from './components/tweet-card/tweet-card.component';
import { HashtagComponent } from './components/hashtag/hashtag.component';
import { NgCircleProgressModule, CircleProgressComponent, CircleProgressOptions } from 'ng-circle-progress';
import { PieSentimentComponent } from './components/pie-sentiment/pie-sentiment.component';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { TweetListComponent } from './components/tweet-list/tweet-list.component';
import { RouterModule } from '@angular/router';
import { AppRoutes } from 'src/app.routes';
import { DashboardComponent } from './components/dashboard/dashboard.component';
@NgModule({
  declarations: [
    AppComponent,
    TweetCardComponent,
    HashtagComponent,
    PieSentimentComponent,
    TweetListComponent,
    DashboardComponent
  ],
  entryComponents: [
    TweetCardComponent
  ],
  imports: [
    RouterModule.forRoot(AppRoutes),
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatCardModule,
    NgCircleProgressModule,
    MatChipsModule,
    MatSelectModule,
    NgxMasonryModule,
    Ng4LoadingSpinnerModule.forRoot()
  ],  
  providers: [BaseHttpService, TweetService, HttpClientModule, CircleProgressOptions],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
