import { Component, OnInit } from '@angular/core';
import { AppComponent } from 'src/app/app.component';
import { Category } from 'src/app/models/category';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {

  categories: Category[]

  constructor(
    private categoriesService: CategoriesService,
    private appComponent: AppComponent
  ) { }

  ngOnInit(): void {
    this.categoriesService.getCategories().subscribe(res => {
      this.categories = res
    })
  }

  deleteCategory(id: number) {
    if (confirm("Вы действительно хотите удалить эту категорию?")) {
      this.categoriesService.deleteCategory(id).subscribe(res => {
        this.categories = this.categories.filter(c => c.id != id)
        this.appComponent.categories = this.appComponent.categories.filter(c => c.id != id)
      }, error => {
        alert("Не удалось удалить категорию")
      })
    }
  }
}
