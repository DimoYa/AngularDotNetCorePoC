import { Component, OnDestroy, OnInit } from "@angular/core";
import { Observable, Subscriber } from "rxjs";
import { AuthorizeService } from "../../../../api-authorization/authorize.service";
import { TrainingModel } from "../../../core/models/training-model";
import { TrainingService } from "../../../core/services/training.service";
import { ConfirmBoxInitializer } from "@costlydeveloper/ngx-awesome-popup";
import { Router } from "@angular/router";
import { PageEvent } from "@angular/material/paginator";

@Component({
  selector: "app-all-trainingns",
  templateUrl: "./all-trainingns.component.html",
  styleUrls: ["./all-trainingns.component.css"],
})
export class AllTrainingnsComponent implements OnInit {
  public isAdmin: Observable<boolean>;
  public isResource: Observable<boolean>;
  public trainings: TrainingModel[];
  public myTrainings: TrainingModel[];
  public pageSlice: TrainingModel[] = [];

  constructor(
    private authorizeService: AuthorizeService,
    private trainingService: TrainingService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isAdmin = this.authorizeService.isAdmin();
    this.isResource = this.authorizeService.isResource();
    this.trainingService.getAllTrainings().subscribe((data) => {
      this.trainings = data;
      this.pageSlice = this.trainings.slice(0, 5);
    });
    this.trainingService.getMyTrainings().subscribe((data) => {
      this.myTrainings = data;
    });
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
          this.trainingService.getAllTrainings().subscribe((data) => {
            this.trainings = data;
          });
        });
      }
      subscription.unsubscribe();
    });
  }

  public isPossibleToAddTraining(trainingId: string): boolean {
    return !this.myTrainings.some((t) => t.id === trainingId);
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

  public OnPageChange(event: PageEvent) {
    const startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize;
    if (endIndex > this.trainings.length) {
      endIndex = this.trainings.length;
    }
    this.pageSlice = this.trainings.slice(startIndex, endIndex);
  }
}