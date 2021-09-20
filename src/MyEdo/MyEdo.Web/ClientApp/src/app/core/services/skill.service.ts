import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SkillCategoryModel } from "../models/skill-model";

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

  public getMySkills(): Observable<SkillCategoryModel[] | null> {
    return this.http.get<SkillCategoryModel[]>(
      this.baseUrl + `${this.skillEndpoint}/GetMySkills`
    );
  }

  public deleteSkill(body: Object): Observable<object> {
    const reqHeader = new HttpHeaders({
      "Content-Type": "application/json",
    });
    const httpOptions = {
      headers: reqHeader,
      body: body,
    };
    const url = `${this.baseUrl}${this.skillEndpoint}`;
    return this.http.delete(url, httpOptions);
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

  public addSkillToMyProfile(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.skillEndpoint}/AddSkillToMyProfile`;
    return this.http.put(url, body);
  }

  public removeSkillFromMyProfile(body: Object): Observable<object> {
    const reqHeader = new HttpHeaders({
      "Content-Type": "application/json",
    });
    const httpOptions = {
      headers: reqHeader,
      body: body,
    };
    const url = `${this.baseUrl}${this.skillEndpoint}/RemoveSkillFromMyProfile`;
    return this.http.delete(url, httpOptions);
  }

  public editSkillLevel(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.skillEndpoint}/EditSkillLevel`;
    return this.http.put(url, body);
  }

  public createCategory(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.skillCategoryEndpoint}`;
    return this.http.post(url, body);
  }
}
