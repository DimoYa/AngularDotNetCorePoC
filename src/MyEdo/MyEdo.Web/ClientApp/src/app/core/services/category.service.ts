import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EditCategoryModel } from '../models/edit-category-model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  skillCategoryEndpoint = "api/SkillCategory";
  httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  public getAllCategories(): Observable<EditCategoryModel[] | null> {
    return this.http.get<EditCategoryModel[]>(
      this.baseUrl + this.skillCategoryEndpoint
    );
  }

  public createCategory(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.skillCategoryEndpoint}`;
    return this.http.post(url, body);
  }

  public editCategory(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.skillCategoryEndpoint}`;
    return this.http.put(url, body);
  }

  public deleteCategory(body: Object): Observable<object> {
    const reqHeader = new HttpHeaders({
      "Content-Type": "application/json",
    });
    const httpOptions = {
      headers: reqHeader,
      body: body,
    };
    const url = `${this.baseUrl}${this.skillCategoryEndpoint}`;
    return this.http.delete(url, httpOptions);
  }
}
