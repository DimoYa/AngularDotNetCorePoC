import { Component, EventEmitter, Inject, OnInit, Output } from "@angular/core";
import { DialogBelonging } from "@costlydeveloper/ngx-awesome-popup";
import { Subscription } from "rxjs";
import { UserTrainingStatus } from "../../../core/models/all-users-trainings-model";
import { TrainingService } from "../../../core/services/training.service";

@Component({
  selector: "app-update-user-training-status",
  templateUrl: "./update-user-training-status.component.html",
  styleUrls: ["./update-user-training-status.component.css"],
})
export class UpdateUserTrainingStatusComponent implements OnInit {

  public data = Object.values(UserTrainingStatus).filter(value => typeof value !== 'number');
  public chosenValue : string
  private subscriptions: Subscription = new Subscription();

  constructor(
    private trainingService: TrainingService,
    @Inject('dialogBelonging') public dialogBelonging: DialogBelonging
  ) {}

  ngOnInit() {
    this.chosenValue = UserTrainingStatus[this.dialogBelonging.CustomData.status]
    this.subscriptions.add(
      this.dialogBelonging.EventsController.onButtonClick$.subscribe(
        (_Button) => {
          if (_Button.ID === "submit") {
            const body = {
              trainingId: this.dialogBelonging.CustomData.trainingId,
              trainingName: this.dialogBelonging.CustomData.trainingName,
              userId: this.dialogBelonging.CustomData.userId,
              userName: this.dialogBelonging.CustomData.userName,
              status: this.chosenValue,
            };

            this.trainingService
              .updateUserTrainingStatus(body)
              .subscribe(() => {});
            this.dialogBelonging.EventsController.close();
          } else if (_Button.ID === "cancel") {
            this.dialogBelonging.EventsController.close();
          }
        }
      )
    );

    setTimeout(() => {
      this.dialogBelonging.EventsController.closeLoader();
    }, 1000);
  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
