import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import UserModel from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  adminEndpoint = 'api/Admin';

  constructor(
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string) {}

    public getAllUsers(): Observable<UserModel[] | null> {
      return this.http.get<UserModel[]>(this.baseUrl + `${this.adminEndpoint}/GetAllUsers`)
    }

    public lockUser(userId: string): Observable<object> {
      return this.http.put(this.baseUrl + `${this.adminEndpoint}/LockUser/${userId}`, {})
    }

    public unLockUser(userId: string): Observable<object> {
      return this.http.put(this.baseUrl + `${this.adminEndpoint}/UnLockUser/${userId}`, {})
    }

    public addRoleToUser(userId: string, roleName: string): Observable<object> {
      return this.http.put(this.baseUrl + `${this.adminEndpoint}/AddRoleToUser/${userId}/${roleName}`, {})
    }

    public removeRoleFromUser(userId: string, roleName: string): Observable<object> {
      return this.http.put(this.baseUrl + `${this.adminEndpoint}/RemoveRoleFromUser/${userId}/${roleName}`, {})
    }
}
