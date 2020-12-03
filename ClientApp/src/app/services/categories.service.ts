import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  private baseApiUrl = `${this.apiUrl}/categories`

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseApiUrl)
  }

  getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.baseApiUrl}/${id}`)
  }

  createCategory(categoryData): Observable<any> {
    return this.http.post(this.baseApiUrl, categoryData)
  }

  updateCategory(id: number, categoryData): Observable<any> {
    return this.http.put(`${this.baseApiUrl}/${id}`, categoryData)
  }

  deleteCategory(id: number): Observable<any> {
    return this.http.delete(`${this.baseApiUrl}/${id}`)
  }
}
