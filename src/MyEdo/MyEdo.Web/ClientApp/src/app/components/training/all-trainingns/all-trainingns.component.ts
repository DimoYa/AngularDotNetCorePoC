import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { AuthorizeService } from "../../../../api-authorization/authorize.service";
import { TrainingModel } from "../../../core/models/training-model";
import { TrainingService } from "../../../core/services/training.service";
import { ConfirmBoxInitializer } from "@costlydeveloper/ngx-awesome-popup";
import { Router } from "@angular/router";

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
    private trainingService: TrainingService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isAdmin = this.authorizeService.isAdmin();
    this.isResource = this.authorizeService.isResource();
    this.trainings$ = this.trainingService.getAllTrainings();
  }

  public deleteTraining(training: TrainingModel) {
    const confirmBox = new ConfirmBoxInitializer();
    confirmBox.setTitle(
      `Are you sure that you want to delete training: ${training.name}?`
    );
    confirmBox.setButtonLabels("YES", "NO");

    const subscription = confirmBox.openConfirmBox$().subscribe((resp) => {
      if (resp.Success) {
        const body = {
          id: training.id,
          name: training.name,
        };

        this.trainingService.deleteTraining(body).subscribe(() => {
          this.trainings$ = this.trainingService.getAllTrainings();
        });
      }
      subscription.unsubscribe();
    });
  }

  public requestTraining(training: TrainingModel) {
    const body = {
      id: training.id,
      name: training.name,
    };

    this.trainingService.requestTraining(body).subscribe(() => {
      this.router.navigate(["/my-trainings"]);
    });
  }
}
