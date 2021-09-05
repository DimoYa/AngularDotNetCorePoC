import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import SkillCategoryModel from '../models/skill-model';

@Injectable({
  providedIn: 'root'
})
export class SkillService {

  skillEndpoint = 'api/Skill';

  constructor(
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string) {}

    public getAllSkills(): Observable<SkillCategoryModel[] | null> {
      return this.http.get<SkillCategoryModel[]>(this.baseUrl + `${this.skillEndpoint}/GetAllSkillsByCategories`)
    }
}
