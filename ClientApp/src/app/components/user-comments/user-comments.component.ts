import { Component, OnInit } from '@angular/core';
import { Comment } from 'src/app/models/comment';
import { CommentsService } from 'src/app/services/comments.service';

@Component({
  selector: 'app-user-comments',
  templateUrl: './user-comments.component.html',
  styleUrls: ['./user-comments.component.scss']
})
export class UserCommentsComponent implements OnInit {

  comments: Comment[];

  constructor(
    private commentsService: CommentsService
  ) { }

  ngOnInit(): void {
    this.commentsService.getUserComments().subscribe(res => {
      this.comments = res
    })
  }

  deleteComment(id: number) {
    if (confirm("Вы действительно хотите удалить этот комментарий?")) {
      this.commentsService.deleteComment(id).subscribe(res => {
        this.comments = this.comments.filter(c => c.id != id)
      }, error => {
        alert("Не удалось удалить комментарий")
      })
    }
  }
}
