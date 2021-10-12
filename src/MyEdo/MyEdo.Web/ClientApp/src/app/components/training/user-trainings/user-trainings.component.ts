import { Component, OnInit } from "@angular/core";
import { AllUsersTrainingsModel } from "../../../core/models/all-users-trainings-model";
import { TrainingService } from "../../../core/services/training.service";
import { PageEvent } from "@angular/material/paginator";

@Component({
  selector: "app-user-trainings",
  templateUrl: "./user-trainings.component.html",
  styleUrls: ["./user-trainings.component.css"],
})
export class UserTrainingsComponent implements OnInit {
  public trainings: AllUsersTrainingsModel[] = [];
  public pageSlice: AllUsersTrainingsModel[] = [];

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
            dueDate: t.dueDate,
          };
          this.trainings.push(currentRecord);
        });
      });
      this.trainings.sort((a, b) => a.userName.localeCompare(b.userName));
      this.pageSlice = this.trainings.slice(0, 5);
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
