import {
  Component,
  OnInit,
  ViewChild,
} from "@angular/core";
import { AllUsersTrainingsModel } from "../../../core/models/all-users-trainings-model";
import { TrainingService } from "../../../core/services/training.service";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import {
  ButtonLayoutDisplay,
  ButtonMaker,
  DialogInitializer,
} from "@costlydeveloper/ngx-awesome-popup";
import { UpdateUserTrainingStatusComponent } from "../update-user-training-status/update-user-training-status.component";
import { MatTableDataSource } from "@angular/material";

@Component({
  selector: "app-user-trainings",
  templateUrl: "./user-trainings.component.html",
  styleUrls: ["./user-trainings.component.css"],
})
export class UserTrainingsComponent implements OnInit {
  public trainings: AllUsersTrainingsModel[] = [];
  public pageSlice: AllUsersTrainingsModel[] = [];

  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator , {static: false})
  paginator: MatPaginator;

  constructor(private trainingService: TrainingService) {}

  ngOnInit() {
    this.ConvertData();
  }

  public OnPageChange(event: PageEvent) {
    const startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize;
    if (endIndex > this.trainings.length) {
      endIndex = this.trainings.length;
    }
    this.pageSlice = this.trainings.slice(startIndex, endIndex);
  }

  public updateUserTrainingStatus(userTraining: AllUsersTrainingsModel) {
    const dialogPopup = new DialogInitializer(
      UpdateUserTrainingStatusComponent
    );

    dialogPopup.setCustomData({
      trainingId: userTraining.trainingId,
      trainingName: userTraining.trainingName,
      userId: userTraining.userId,
      userName: userTraining.userName,
      status: userTraining.status,
    });

    dialogPopup.setButtons([
      new ButtonMaker("Submit", "submit", ButtonLayoutDisplay.SUCCESS),
      new ButtonMaker("Cancel", "cancel", ButtonLayoutDisplay.SECONDARY),
    ]);

    const subscription = dialogPopup.openDialog$().subscribe(() => {
      subscription.unsubscribe();

      setTimeout(() => {
        this.trainings = [];
        this.pageSlice = [];
        this.ConvertData();
        this.paginator.firstPage();
      }, 1000);
    });
  }

  private ConvertData() {
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
}
