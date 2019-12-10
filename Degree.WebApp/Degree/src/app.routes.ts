import { Routes } from '@angular/router';
import { TweetGridComponent } from './app/components/tweet-grid/tweet-grid.component';
import { DashboardComponent } from './app/components/dashboard/dashboard.component';

export const AppRoutes: Routes = [
    { path: '', component: DashboardComponent},
    { path: 'wcf-tweets', component: TweetGridComponent },
  ];