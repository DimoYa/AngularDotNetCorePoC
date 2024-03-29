import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EditSkillModel } from "../models/edit-skill-model";
import { SkillCategoryModel } from "../models/skill-model";

@Injectable({
  providedIn: "root",
})
export class SkillService {
  skillEndpoint = "api/Skill";
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

  public getSkillById(id: string): Observable<EditSkillModel | null> {
    return this.http.get<EditSkillModel>(
      this.baseUrl + `${this.skillEndpoint}/GetSkillById/${id}`
    );
  }

  public createSkill(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.skillEndpoint}`;
    return this.http.post(url, body);
  }

  public editSkill(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.skillEndpoint}`;
    return this.http.put(url, body);
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
}