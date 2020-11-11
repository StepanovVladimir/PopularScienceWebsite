import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Article } from 'src/app/models/article';
import { Comment } from 'src/app/models/comment';
import { ArticlesService } from 'src/app/services/articles.service';
import { AuthService } from 'src/app/services/auth.service';
import { CommentsService } from 'src/app/services/comments.service';
import { LikesService } from 'src/app/services/likes.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {

  article: Article
  likeIsPutted: boolean
  comments: Comment[]
  commentsEdits: boolean[]
  commentsEditTexts: string[]

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private articlesService: ArticlesService,
    private likesService: LikesService,
    private commentsService: CommentsService
  ) { }

  ngOnInit(): void {
    this.articlesService.getArticle(this.route.snapshot.params.articleId)
      .subscribe(res => {
        this.article = res
        this.refreshComments()
        if (this.isLoggedIn)
        {
          this.likesService.likeIsPutted(this.article.id)
            .subscribe(res => {
              this.likeIsPutted = res.putted
            })
        }
      })
  }

  refreshComments() {
    this.commentsService.getArticleComments(this.article.id)
      .subscribe(res => {
        this.comments = res
        this.commentsEdits = Array(this.comments.length).fill(false)
        this.commentsEditTexts = this.comments.map(c => c.text)
      })
  }

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated()
  }

  get isModerator(): boolean {
    return this.authService.isModerator()
  }

  get userId(): number {
    return this.authService.getUserId()
  }

  putLike() {
    this.likesService.putLike(this.article.id)
      .subscribe(res => {
        this.article.likesCount = res.count
        this.likeIsPutted = true
      }, error => {
        alert("Не удалось поставить лайк")
      })
  }

  cancelLike() {
    this.likesService.cancelLike(this.article.id)
      .subscribe(res => {
        this.article.likesCount = res.count
        this.likeIsPutted = false
      }, error => {
        alert("Не удалось отменить лайк")
      })
  }

  showEditComment(index: number) {
    this.commentsEdits[index] = true
  }

  closeEditComment(index: number) {
    this.commentsEdits[index] = false
  }

  createComment(form: NgForm) {
    this.commentsService.createComment(form.value.text, this.article.id)
      .subscribe(res => {
        this.refreshComments()
        form.resetForm()
      }, error => {
        alert("Не удалось добавить комментарий")
      })
  }

  updateComment(id: number, form: NgForm) {
    this.commentsService.updateComment(id, form.value.text)
      .subscribe(res => {
        this.refreshComments()
        form.resetForm()
      }, error => {
        alert("Не удалось добавить комментарий")
      })
  }

  deleteComment(id: number) {
    if (confirm("Вы действительно хотите удалить этот комментарий?")) {
      this.commentsService.deleteComment(id)
        .subscribe(res => {
          this.refreshComments()
        }, error => {
          alert("Не удалось удалить комментарий")
        })
    }
  }
}
