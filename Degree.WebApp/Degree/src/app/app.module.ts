import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BaseHttpService } from 'src/services/base-http.service';
import { TweetService } from 'src/services/tweet.service';
import { HttpClientModule } from '@angular/common/http';
import {
  MatCardModule
} from '@angular/material';
import { NgxMasonryModule } from 'ngx-masonry';
import { TweetCardComponent } from './components/tweet-card/tweet-card.component';
import { HashtagComponent } from './components/hashtag/hashtag.component';
@NgModule({
  declarations: [
    AppComponent,
    TweetCardComponent,
    HashtagComponent,
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
    NgxMasonryModule
  ],  
  providers: [BaseHttpService, TweetService, HttpClientModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
