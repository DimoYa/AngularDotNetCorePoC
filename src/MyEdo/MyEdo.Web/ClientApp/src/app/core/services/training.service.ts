import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { TrainingModel } from "../models/training-model";

@Injectable({
  providedIn: "root",
})
export class TrainingService {
  trainingEndPoint = "api/Training";
  httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  public getAllTrainings(): Observable<TrainingModel[] | null> {
    return this.http.get<TrainingModel[]>(this.baseUrl + this.trainingEndPoint);
  }

  public getMyTrainings(): Observable<TrainingModel[] | null> {
    return this.http.get<TrainingModel[]>(`${this.baseUrl}${this.trainingEndPoint}/GetMyTrainings`);
  }

  public createTraining(body: Object): Observable<object> {
    const url = this.baseUrl + this.trainingEndPoint;
    return this.http.post(url, body);
  }

  public editTraining(body: Object): Observable<object> {
    const url = this.baseUrl + this.trainingEndPoint;
    return this.http.put(url, body);
  }

  public deleteTraining(body: Object): Observable<object> {
    const reqHeader = new HttpHeaders({
      "Content-Type": "application/json",
    });
    const httpOptions = {
      headers: reqHeader,
      body: body,
    };
    const url = this.baseUrl + this.trainingEndPoint;
    return this.http.delete(url, httpOptions);
  }

  public requestTraining(body: Object): Observable<object> {
    const url = `${this.baseUrl}${this.trainingEndPoint}/RequestTraining`;
    return this.http.post(url, body);
  }
}
