import { Component } from '@angular/core';
import { Category } from './models/category';
import { AuthService } from './services/auth.service';
import { CategoriesService } from './services/categories.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  
  public categories: Category[]

  constructor(
    private authService: AuthService,
    private categoriesService: CategoriesService
  ) { }

  ngOnInit(): void {
    this.refreshCategories()
  }

  public refreshCategories() {
    this.categoriesService.getCategories().subscribe(res => {
      this.categories = res
    })
  }

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated()
  }

  logout() {
    this.authService.logout()
  }
}
