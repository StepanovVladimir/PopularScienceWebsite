import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { Comment } from '../models/comment';
import { Count } from '../models/count';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  private baseApiUrl = `${this.apiUrl}/comments`

  getCommentsCount(articleId: number): Observable<Count> {
    return this.http.get<Count>(`${this.baseApiUrl}/article/${articleId}/count`)
  }

  getArticleComments(articleId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.baseApiUrl}/article/${articleId}`)
  }

  getUserComments(): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.baseApiUrl}/user`)
  }

  getComments(): Observable<Comment[]> {
    return this.http.get<Comment[]>(this.baseApiUrl)
  }

  createComment(text: string, articleId: number): Observable<any> {
    return this.http.post(this.baseApiUrl, { text, articleId })
  }

  updateComment(id: number, text: string): Observable<any> {
    return this.http.put(`${this.baseApiUrl}/${id}`, { text })
  }

  deleteComment(id: number): Observable<any> {
    return this.http.delete(`${this.baseApiUrl}/${id}`)
  }
}
