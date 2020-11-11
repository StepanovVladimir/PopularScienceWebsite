import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  login(form: NgForm) {
    this.authService.login(form.value)
      .subscribe(res => { }, error => {
        alert('Не правильный логин или пароль')
      })
  }
}
