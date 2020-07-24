import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../_service/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../_models/user';
import { UserService } from '../../_service/user.service';
import { from } from 'rxjs';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users: User[];
  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data.users;
    });
  }

  loadUsers() {
    this.userService.getUsers()
      .subscribe((users: User[]) => {
        this.users = users;
       // this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

}
