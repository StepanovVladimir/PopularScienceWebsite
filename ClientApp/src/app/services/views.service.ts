import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { Count } from '../models/count';

@Injectable({
  providedIn: 'root'
})
export class ViewsService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  private baseApiUrl = `${this.apiUrl}/views`

  getViewsCount(articleId: number): Observable<Count> {
    return this.http.get<Count>(`${this.baseApiUrl}/${articleId}`)
  }
}
