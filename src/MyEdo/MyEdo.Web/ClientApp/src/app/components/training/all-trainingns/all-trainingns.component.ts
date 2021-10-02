import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { AuthorizeService } from "../../../../api-authorization/authorize.service";
import { TrainingModel } from "../../../core/models/training-model";
import { TrainingService } from "../../../core/services/training.service";

@Component({
  selector: "app-all-trainingns",
  templateUrl: "./all-trainingns.component.html",
  styleUrls: ["./all-trainingns.component.css"],
})
export class AllTrainingnsComponent implements OnInit {
  public isAdmin: Observable<boolean>;
  public isResource: Observable<boolean>;
  public trainings$: Observable<TrainingModel[]>;

  constructor(
    private authorizeService: AuthorizeService,
    private trainingService: TrainingService
  ) {}

  ngOnInit() {
    this.isAdmin = this.authorizeService.isAdmin();
    this.isResource = this.authorizeService.isResource();
    this.trainings$ = this.trainingService.getAllTrainings();
  }
}
