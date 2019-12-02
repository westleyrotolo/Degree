import { Routes } from '@angular/router';
import { TweetListComponent } from './app/components/tweet-list/tweet-list.component';
import { DashboardComponent } from './app/components/dashboard/dashboard.component';

export const AppRoutes: Routes = [
    { path: '', component: DashboardComponent},
    { path: 'wcf-tweets', component: TweetListComponent },
  ];