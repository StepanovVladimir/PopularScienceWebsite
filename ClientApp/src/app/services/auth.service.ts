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

  login(name: string, password: string): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}auth/login`, {
      name, password
    }).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.accessToken)
        this.router.navigate(['/home'])
      })
    )
  }

  register(name: string, password: string, passwordConfirm: string): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}auth/register`, {
      name, password, passwordConfirm
    }).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.accessToken)
        this.router.navigate(['/home'])
      })
    )
  }

  isAuthenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY)
    return token && !this.jwtHelper.isTokenExpired(token)
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY)
    this.router.navigate([''])
  }
}
