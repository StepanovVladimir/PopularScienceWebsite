import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { Article } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  private baseApiUrl = `${this.apiUrl}articles/`

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.baseApiUrl}`)
  }

  getFavouriteArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.baseApiUrl}favourite`)
  }
}
