import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  private baseApiUrl = `${this.apiUrl}/users`

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseApiUrl)
  }

  giveRights(id: number): Observable<any> {
    return this.http.post(`${this.baseApiUrl}/${id}`, null)
  }

  depriveRights(id: number): Observable<any> {
    return this.http.delete(`${this.baseApiUrl}/${id}`)
  }
}
