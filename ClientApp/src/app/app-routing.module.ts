import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { FavouriteArticlesComponent } from './components/favourite-articles/favourite-articles.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ArticleComponent } from './components/article/article.component';
import { CreateArticleComponent } from './components/create-article/create-article.component';
import { AdminGuard } from './guards/admin.guard';
import { EditArticleComponent } from './components/edit-article/edit-article.component';
import { CategoryArticlesComponent } from './components/category-articles/category-articles.component';
import { UserCommentsComponent } from './components/user-comments/user-comments.component';
import { CreateCategoryComponent } from './components/create-category/create-category.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { EditCategoryComponent } from './components/edit-category/edit-category.component';
import { SeenArticlesComponent } from './components/seen-articles/seen-articles.component';
import { CommentsComponent } from './components/comments/comments.component';
import { ModeratorGuard } from './guards/moderator.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '', component: ArticlesComponent },
  { path: 'article/show/:id', component: ArticleComponent },
  { path: 'articles/category/:id', component: CategoryArticlesComponent },
  { path: 'home/seen', component: SeenArticlesComponent, canActivate: [AuthGuard] },
  { path: 'home/favourite', component: FavouriteArticlesComponent, canActivate: [AuthGuard] },
  { path: 'home/comments', component: UserCommentsComponent, canActivate: [AuthGuard] },
  { path: 'comments', component: CommentsComponent, canActivate: [ModeratorGuard] },
  { path: 'article/create', component: CreateArticleComponent, canActivate: [AdminGuard] },
  { path: 'article/edit/:id', component: EditArticleComponent, canActivate: [AdminGuard] },
  { path: 'categories', component: CategoriesComponent, canActivate: [AdminGuard] },
  { path: 'category/create', component: CreateCategoryComponent, canActivate: [AdminGuard] },
  { path: 'category/edit/:id', component: EditCategoryComponent, canActivate: [AdminGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
