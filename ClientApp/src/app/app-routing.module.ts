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

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '', component: ArticlesComponent },
  { path: 'article/show/:id', component: ArticleComponent },
  { path: 'articles/category/:id', component: CategoryArticlesComponent },
  { path: 'home/favourite', component: FavouriteArticlesComponent, canActivate: [AuthGuard] },
  { path: 'article/create', component: CreateArticleComponent, canActivate: [AdminGuard] },
  { path: 'article/edit/:id', component: EditArticleComponent, canActivate: [AdminGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
