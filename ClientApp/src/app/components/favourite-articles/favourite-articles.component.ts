import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-favourite-articles',
  templateUrl: './favourite-articles.component.html',
  styleUrls: ['./favourite-articles.component.scss']
})
export class FavouriteArticlesComponent implements OnInit {

  articles: Article[] = []
  columns = ['id', 'title', 'description', 'image', 'createdAt']

  constructor(
    private articlesService: ArticlesService
  ) { }

  ngOnInit(): void {
    this.articlesService.getFavouriteArticles()
      .subscribe(res => {
        this.articles = res
      })
  }
}
