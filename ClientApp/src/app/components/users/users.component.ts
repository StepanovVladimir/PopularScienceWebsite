import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  users: User[]

  constructor(
    private usersService: UsersService
  ) { }

  ngOnInit(): void {
    this.usersService.getUsers().subscribe(res => {
      this.users = res
    })
  }

  giveRights(id: number) {
    this.usersService.giveRights(id).subscribe(res => {
      this.users.find(u => u.id == id).isModerator = true
    })
  }

  depriveRights(id: number) {
    this.usersService.depriveRights(id).subscribe(res => {
      this.users.find(u => u.id == id).isModerator = false
    })
  }
}
