import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-seen-articles',
  templateUrl: './seen-articles.component.html',
  styleUrls: ['./seen-articles.component.scss']
})
export class SeenArticlesComponent implements OnInit {

  articles: Article[]

  constructor(
    private articlesService: ArticlesService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.articlesService.getSeenArticles().subscribe(res =>
      this.articles = res
    )
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
