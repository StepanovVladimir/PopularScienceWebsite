import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { Category } from 'src/app/models/category';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent implements OnInit {

  category: Category
  categoryName: string

  constructor(
    private route: ActivatedRoute,
    private categoriesService: CategoriesService,
    private appComponent: AppComponent,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.categoriesService.getCategory(this.route.snapshot.params.id).subscribe(res => {
      this.category = res
      this.categoryName = res.name
    })
  }

  updateCategory(form: NgForm) {
    this.categoriesService.updateCategory(this.category.id, form.value).subscribe(res => {
      this.appComponent.refreshCategories()
      this.router.navigate(['/categories'])
    }, error => {
      alert("Не удалось обновить категорию")
    })
  }
}
