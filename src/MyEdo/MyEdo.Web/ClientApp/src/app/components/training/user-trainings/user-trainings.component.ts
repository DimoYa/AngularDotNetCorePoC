import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { AllUsersTrainingsModel } from "../../../core/models/all-users-trainings-model";
import { TrainingService } from "../../../core/services/training.service";

@Component({
  selector: "app-user-trainings",
  templateUrl: "./user-trainings.component.html",
  styleUrls: ["./user-trainings.component.css"],
})
export class UserTrainingsComponent implements OnInit {
  public trainings: AllUsersTrainingsModel[] = []
  constructor(private trainingService: TrainingService) {}

  ngOnInit() {
    this.trainingService.getAllUsersTrainings().subscribe((data) => {
      data.forEach((d) => {
        d.trainings.forEach((t) => {
          let currentRecord: AllUsersTrainingsModel = {
            trainingId: t.trainingId,
            trainingName: t.trainingName,
            userId: d.userId,
            userName: d.userName,
            status: t.status,
            dueDate: t.dueDate
          };
          this.trainings.push(currentRecord);
        });
      });
      this.trainings.sort((a, b) => a.userName.localeCompare(b.userName));
    });
  }
}
