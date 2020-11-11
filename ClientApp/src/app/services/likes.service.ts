import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { Count } from '../models/count';
import { Putted } from '../models/putted';

@Injectable({
  providedIn: 'root'
})
export class LikesService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  private baseApiUrl = `${this.apiUrl}/likes`

  getLikesCount(articleId: number): Observable<Count> {
    return this.http.get<Count>(`${this.baseApiUrl}/${articleId}/count`)
  }

  likeIsPutted(articleId: number): Observable<Putted> {
    return this.http.get<Putted>(`${this.baseApiUrl}/${articleId}/putted`)
  }

  putLike(articleId: number): Observable<Count> {
    return this.http.post<Count>(`${this.baseApiUrl}/${articleId}`, null)
  }

  cancelLike(articleId: number): Observable<Count> {
    return this.http.delete<Count>(`${this.baseApiUrl}/${articleId}`)
  }
}
