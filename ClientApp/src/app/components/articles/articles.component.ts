import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {

  articles: Article[]

  constructor(
    private articlesService: ArticlesService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.articlesService.getArticles().subscribe(res => {
      this.articles = res
    })
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
