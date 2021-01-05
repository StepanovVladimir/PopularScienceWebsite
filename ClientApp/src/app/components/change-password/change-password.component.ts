import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  register(form: NgForm) {
    if (form.value.newPassword != form.value.newPasswordConfirm) {
      alert("Пароли не совпадают")
      return
    }

    this.authService.changePassword(form.value)
      .subscribe(res => {
        alert("Пароль успешно изменён")
      }, error => {
        alert("Старый пароль не верен")
      })
  }
}
