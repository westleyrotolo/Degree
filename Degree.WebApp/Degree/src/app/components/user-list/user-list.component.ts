import { Component, OnInit } from '@angular/core';
import { TweetService } from 'src/services/tweet.service';
import { User, UserList } from 'src/models/user';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  constructor(private tweetService: TweetService,
    private spinner: Ng4LoadingSpinnerService) { }

  userSelections: string[] = [
    "Utenti più Attivi",
    "Utenti più Retwittati",
    "Utenti con più Like"
  ]
  userType: UserList = UserList.Active;
  orderChanged($event) {
    console.log(this.select);
    if (this.select == "Utenti più Attivi") {
      this.userType = UserList.Active;
      this.spinner.show();
      if (this.usersActive.length == 0) {
        this.tweetService.fetchUserMoreActives().subscribe((resp) => {
          console.log(resp);
          this.usersActive = resp;
          this.usersActive = this.users;
          this.spinner.hide();
        });
      } else {
        this.users = this.usersActive;
        this.spinner.hide();
      }
    } else if (this.select == "Utenti più Retwittati") {
      this.userType = UserList.Retweet;
      this.spinner.show();
      if (this.usersRetweet.length == 0) {
        this.tweetService.fetchUserMoreRetweets().subscribe((resp) => {
          console.log(resp);
          this.usersRetweet = resp;
          this.users = this.usersRetweet;
          this.spinner.hide();
        });
      } else {
        this.users = this.usersRetweet;
        this.spinner.hide();

      }
    } else if (this.select == "Utenti con più Like") {
      this.userType = UserList.Like;
      this.spinner.show();
      if (this.usersLike.length == 0) {
        this.tweetService.fetchUserMoreFavorites().subscribe((resp) => {
          console.log(resp);
          this.usersLike = resp;
          this.users = this.usersLike;
          this.spinner.hide();
        });
      } else {
        this.users = this.usersLike;
        this.spinner.hide();
      }
    }

  }
  select = "Utenti più Attivi";
  usersActive: User[] = [];
  usersRetweet: User[] = [];
  usersLike: User[] = [];
  users: User[] = [];
  ngOnInit() {
    this.tweetService.fetchUserMoreActives().subscribe((resp) => {
      console.log(resp);
      this.usersActive = resp;
      this.users = this.usersActive;
    });
  }

}