import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  register(name: string, password: string, passwordConfirm) {
    this.authService.register(name, password, passwordConfirm)
      .subscribe(res => {

      }, error => {
        alert('Ошибка регистрации')
      })
  }
}
