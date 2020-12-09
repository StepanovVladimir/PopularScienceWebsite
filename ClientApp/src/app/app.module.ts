import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http'

import { HomeComponent } from './components/home/home.component';
import { ArticlesComponent } from './components/articles/articles.component'
import { API_URL } from './app-injection-tokens';
import { environment } from 'src/environments/environment';
import { JwtModule } from '@auth0/angular-jwt';
import { ACCESS_TOKEN_KEY } from './services/auth.service';
import { FavouriteArticlesComponent } from './components/favourite-articles/favourite-articles.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ArticleComponent } from './components/article/article.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { CreateArticleComponent } from './components/create-article/create-article.component';
import { EditArticleComponent } from './components/edit-article/edit-article.component';
import { CategoryArticlesComponent } from './components/category-articles/category-articles.component';
import { UserCommentsComponent } from './components/user-comments/user-comments.component';
import { CreateCategoryComponent } from './components/create-category/create-category.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { EditCategoryComponent } from './components/edit-category/edit-category.component';
import { SeenArticlesComponent } from './components/seen-articles/seen-articles.component';
import { CommentsComponent } from './components/comments/comments.component';
import { UsersComponent } from './components/users/users.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ArticlesComponent,
    FavouriteArticlesComponent,
    LoginComponent,
    RegisterComponent,
    ArticleComponent,
    CreateArticleComponent,
    EditArticleComponent,
    CategoryArticlesComponent,
    UserCommentsComponent,
    CreateCategoryComponent,
    CategoriesComponent,
    EditCategoryComponent,
    SeenArticlesComponent,
    CommentsComponent,
    UsersComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularEditorModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenAllowedDomains
      }
    })
  ],
  providers: [{
    provide: API_URL,
    useValue: environment.api
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
