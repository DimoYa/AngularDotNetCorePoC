import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import SkillCategoryModel from "../models/skill-model";

@Injectable({
  providedIn: "root",
})
export class SkillService {
  skillEndpoint = "api/Skill";
  skillCategoryEndpoint = "api/SkillCategory";
  httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  public getAllSkills(): Observable<SkillCategoryModel[] | null> {
    return this.http.get<SkillCategoryModel[]>(
      this.baseUrl + `${this.skillEndpoint}/GetAllSkillsByCategories`
    );
  }

  public deleteCategory(body: Object): Observable<object> {
    const reqHeader = new HttpHeaders({
      "Content-Type": "application/json",
    });
    const httpOptions = {
      headers: reqHeader,
      body: body,
    };
    const url = this.baseUrl + `${this.skillCategoryEndpoint}`;
    return this.http.delete(url, httpOptions);
  }
}
