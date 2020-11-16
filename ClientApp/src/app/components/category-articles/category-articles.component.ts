import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-category-articles',
  templateUrl: './category-articles.component.html',
  styleUrls: ['./category-articles.component.scss']
})
export class CategoryArticlesComponent implements OnInit {

  articles: Article[]

  constructor(
    private route: ActivatedRoute,
    private articlesService: ArticlesService,
    private authService: AuthService
  ) {
    this.route.params.subscribe(params => {
      this.articlesService.getCategoryArticles(params.id).subscribe(res => {
        this.articles = res
      })
    });
  }

  ngOnInit(): void {
    /*this.articlesService.getCategoryArticles(this.route.snapshot.params.id).subscribe(res => {
      this.articles = res
    })*/
  }

  get isAdmin(): boolean {
    return this.authService.isAdmin()
  }

  deleteArticle(id: number) {
    if (confirm("Вы действительно хотите удалить эту статью?")) {
      this.articlesService.deleteArticle(id).subscribe(res => {
        this.articles = this.articles.filter(a => a.id != id)
      }, error => {
        alert("Не удалось удалить статью")
      })
    }
  }
}
