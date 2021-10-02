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
}
