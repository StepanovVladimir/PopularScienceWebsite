import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { API_URL } from '../app-injection-tokens';
import { Article } from '../models/article';
import { CommentsService } from './comments.service';
import { LikesService } from './likes.service';
import { ViewsService } from './views.service';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    private viewsService: ViewsService,
    private likesService: LikesService,
    private commentsService: CommentsService
  ) { }

  private baseApiUrl = `${this.apiUrl}/articles`

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.baseApiUrl).pipe(tap(articles => {
        articles.forEach(a => {
          this.viewsService.getViewsCount(a.id).subscribe(res => a.viewsCount = res.count)
          this.likesService.getLikesCount(a.id).subscribe(res => a.likesCount = res.count)
          this.commentsService.getCommentsCount(a.id).subscribe(res => a.commentsCount = res.count)
        })
      })
    )
  }

  getFavouriteArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.baseApiUrl}/favourite`).pipe(tap(articles => {
      articles.forEach(a => {
        this.viewsService.getViewsCount(a.id).subscribe(res => a.viewsCount = res.count)
        this.likesService.getLikesCount(a.id).subscribe(res => a.likesCount = res.count)
        this.commentsService.getCommentsCount(a.id).subscribe(res => a.commentsCount = res.count)
      })
    }))
  }

  getArticle(id: number): Observable<Article> {
    return this.http.get<Article>(`${this.baseApiUrl}/${id}`).pipe(tap(a => {
      this.viewsService.getViewsCount(a.id).subscribe(res => a.viewsCount = res.count)
      this.likesService.getLikesCount(a.id).subscribe(res => a.likesCount = res.count)
      this.commentsService.getCommentsCount(a.id).subscribe(res => a.commentsCount = res.count)
    }))
  }
}
