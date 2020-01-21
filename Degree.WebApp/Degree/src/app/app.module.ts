import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BaseHttpService } from 'src/services/base-http.service';
import { TweetService } from 'src/services/tweet.service';
import { HttpClientModule } from '@angular/common/http';
import {
  MatCardModule, MatChipsModule, MatChipList, MatSelectModule, MatTableModule, MatDialogModule
} from '@angular/material';
import { NgxMasonryModule } from 'ngx-masonry';
import { TweetCardComponent } from './components/tweet-card/tweet-card.component';
import { HashtagComponent } from './components/hashtag/hashtag.component';
import { NgCircleProgressModule, CircleProgressComponent, CircleProgressOptions } from 'ng-circle-progress';
import { PieSentimentComponent } from './components/pie-sentiment/pie-sentiment.component';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { TweetGridComponent } from './components/tweet-grid/tweet-grid.component';
import { RouterModule } from '@angular/router';
import { AppRoutes } from 'src/app.routes';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ScrollToBottomDirective } from 'src/directives/scroll-to-bottom.directive';
import { UserCardComponent } from './components/user-card/user-card.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { TweetListComponent } from './components/tweet-list/tweet-list.component';
import { WordCloudComponent, DialogOverview } from './components/word-cloud/word-cloud.component';
import { WordSentimentComponent } from './components/word-sentiment/word-sentiment.component';
import { ExperimentalComponent } from './components/experimental/experimental.component';
import { RadialSentimentComponent } from './components/radial-sentiment/radial-sentiment.component';
import { ChartsModule } from 'ng2-charts';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    ScrollToBottomDirective,
    TweetCardComponent,
    HashtagComponent,
    PieSentimentComponent,
    TweetGridComponent,
    DashboardComponent,
    UserCardComponent,
    UserListComponent,
    TweetListComponent,
    WordCloudComponent,
    WordSentimentComponent,
    DialogOverview,
    ExperimentalComponent,
    RadialSentimentComponent
  ],
  entryComponents: [
    TweetCardComponent,
    DialogOverview
  ],
  imports: [
    RouterModule.forRoot(AppRoutes),
    BrowserModule,
    ChartsModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatCardModule,
    NgCircleProgressModule,
    MatChipsModule,
    MatTableModule,
    MatDialogModule,
    MatSelectModule,
    NgxMasonryModule,
    Ng4LoadingSpinnerModule.forRoot()
  ],  
  providers: [BaseHttpService, TweetService, HttpClientModule, CircleProgressOptions],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
