import { Component, OnInit, Input } from '@angular/core';
import { User, UserList } from 'src/models/user';
@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent implements OnInit {

  @Input()
  user: User
  @Input()
  type: UserList;
  profilePic: string;
  count: number;
  constructor() { }

  ngOnInit() {
    this.profilePic = `url(${this.user.profileImage})`;
    console.log('user:', this.user);
    this.setType();
  }
  setType() {
    if (this.type == UserList.Active){
      this.count = this.user.statuses;
    } else if (this.type == UserList.Like) {
      this.count = this.user.favorites;
    } else if (this.type == UserList.Retweet) {
      this.count = this.user.retweets;
    }
  }
}
