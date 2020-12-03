import { Component, OnInit } from '@angular/core';
import { Comment } from 'src/app/models/comment';
import { CommentsService } from 'src/app/services/comments.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {

  comments: Comment[];

  constructor(
    private commentsService: CommentsService
  ) { }

  ngOnInit(): void {
    this.refreshComments()
  }

  refreshComments() {
    this.commentsService.getComments().subscribe(res => {
      this.comments = res
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
