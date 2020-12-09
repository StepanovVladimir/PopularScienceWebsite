import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { API_URL } from '../app-injection-tokens';
import { Token } from '../models/token';

export const ACCESS_TOKEN_KEY = 'access_token'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  private baseApiUrl = `${this.apiUrl}/auth`

  login(loginData): Observable<Token> {
    return this.http.post<Token>(`${this.baseApiUrl}/login`, loginData).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.accessToken)
        this.router.navigate(['/home'])
      })
    )
  }

  register(registerData): Observable<Token> {
    return this.http.post<Token>(`${this.baseApiUrl}/register`, registerData).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.accessToken)
        this.router.navigate(['/home'])
      })
    )
  }

  changePassword(changePasswordData): Observable<any> {
    return this.http.put(`${this.baseApiUrl}/password/change`, changePasswordData).pipe(
      tap(res => {
        this.router.navigate(['/home'])
      })
    )
  }

  isAuthenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY)
    return token && !this.jwtHelper.isTokenExpired(token)
  }

  getUserId(): number {
    if (!this.isAuthenticated()) {
      return 0
    }

    return Number.parseInt(this.jwtHelper.decodeToken(localStorage.getItem(ACCESS_TOKEN_KEY)).sub)
  }

  isModerator(): boolean {
    if (!this.isAuthenticated()) {
      return false
    }

    let role = this.jwtHelper.decodeToken(localStorage.getItem(ACCESS_TOKEN_KEY)).role
    if (!role) {
      return false
    }

    if (typeof(role) == "string") {
      return role == "Moderator"
    } else {
      return role.includes("Moderator")
    }
  }

  isAdmin(): boolean {
    if (!this.isAuthenticated()) {
      return false
    }

    let role = this.jwtHelper.decodeToken(localStorage.getItem(ACCESS_TOKEN_KEY)).role
    if (!role) {
      return false
    }

    if (typeof(role) == "string") {
      return role == "Admin"
    } else {
      return role.includes("Admin")
    }
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY)
    this.router.navigate([''])
  }
}
