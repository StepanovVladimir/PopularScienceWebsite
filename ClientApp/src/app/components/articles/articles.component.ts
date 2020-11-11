import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { CommentsService } from 'src/app/services/comments.service';
import { LikesService } from 'src/app/services/likes.service';
import { ViewsService } from 'src/app/services/views.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {

  articles: Article[] = []

  constructor(
    private articlesService: ArticlesService,
    private viewsService: ViewsService,
    private likesService: LikesService,
    private commentsService: CommentsService
  ) { }

  ngOnInit(): void {
    this.articlesService.getArticles()
      .subscribe(res => {
        this.articles = res
      })
  }
}
