import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.scss']
})
export class CreateCategoryComponent implements OnInit {

  constructor(
    private categoriesService: CategoriesService,
    private appComponent: AppComponent,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  createCategory(form: NgForm) {
    this.categoriesService.createCategory(form.value).subscribe(res => {
      this.appComponent.refreshCategories()
      this.router.navigate(['/categories'])
    }, error => {
      alert("Не удалось добавить категорию")
    })
  }
}
