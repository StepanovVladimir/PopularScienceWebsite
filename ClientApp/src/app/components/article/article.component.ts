import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
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

  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: 'auto',
    minHeight: '100',
    maxHeight: 'auto',
    width: 'auto',
    minWidth: '0',
    translate: 'yes',
    enableToolbar: false,
    showToolbar: false,
  }

  article: Article
  comments: Comment[]
  commentForm: FormGroup
  commentsEdits: boolean[]
  commentsForms: FormGroup[]

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private articlesService: ArticlesService,
    private likesService: LikesService,
    private commentsService: CommentsService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.articlesService.getArticle(this.route.snapshot.params.id).subscribe(res => {
      this.article = res
      this.refreshComments()
    })
    
    this.commentForm = this.formBuilder.group({
      text: ['', [Validators.required]]
    })
  }

  refreshComments() {
    this.commentsService.getArticleComments(this.article.id).subscribe(res => {
      console.log(this.commentsForms)
      this.comments = res
      this.article.commentsCount = this.comments.length
      this.commentsEdits = Array(this.comments.length).fill(false)
      this.commentsForms = this.comments.map(c => this.formBuilder.group({
        text: [c.text, [Validators.required]]
      }))
    })
  }

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated()
  }

  get isModerator(): boolean {
    return this.authService.isModerator()
  }

  get isAdmin(): boolean {
    return this.authService.isAdmin()
  }

  get userId(): number {
    return this.authService.getUserId()
  }

  deleteArticle() {
    if (confirm("Вы действительно хотите удалить эту статью?")) {
      this.articlesService.deleteArticle(this.article.id).subscribe(res => {
        this.router.navigate([''])
      }, error => {
        alert("Не удалось удалить статью")
      })
    }
  }

  putLike() {
    this.likesService.putLike(this.article.id).subscribe(res => {
      this.article.likesCount = res.count
      this.article.likeIsPutted = true
    }, error => {
      alert("Не удалось поставить лайк")
    })
  }

  cancelLike() {
    this.likesService.cancelLike(this.article.id).subscribe(res => {
      this.article.likesCount = res.count
      this.article.likeIsPutted = false
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

  createComment() {
    this.commentsService.createComment(this.commentForm.value.text, this.article.id).subscribe(res => {
      this.refreshComments()
      this.commentForm.reset()
    }, error => {
      alert("Не удалось добавить комментарий")
    })
  }

  updateComment(id: number, index: number) {
    this.commentsService.updateComment(id, this.commentsForms[index].value.text).subscribe(res => {
      this.refreshComments()
    }, error => {
      alert("Не удалось изменить комментарий")
    })
  }

  deleteComment(id: number) {
    if (confirm("Вы действительно хотите удалить этот комментарий?")) {
      this.commentsService.deleteComment(id).subscribe(res => {
        this.refreshComments()
      }, error => {
        alert("Не удалось удалить комментарий")
      })
    }
  }
}
