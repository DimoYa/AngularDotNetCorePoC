import { Component, OnDestroy, OnInit } from "@angular/core";
import { DialogBelonging } from "@costlydeveloper/ngx-awesome-popup";
import { Subscription } from "rxjs";
import { SkillService } from "../../../core/services/skill.service";

@Component({
  selector: "app-add-skill",
  templateUrl: "./add-skill.component.html",
  styleUrls: ["./add-skill.component.css"],
})
export class AddSkillComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription = new Subscription();

  constructor(
    private skillService: SkillService,
    public dialogBelonging: DialogBelonging
  ) {}

  selectedLevel: number;
  data: number[] = [1, 2, 3, 4, 5];

  ngOnInit() {
    console.log(this.dialogBelonging);

    this.subscriptions.add(
      // IDialogEventsController
      this.dialogBelonging.EventsController.onButtonClick$.subscribe(
        (_Button) => {
          if (_Button.ID === "submit") {
            const body = {
              skillId: this.dialogBelonging.CustomData.id,
              skillName: this.dialogBelonging.CustomData.name,
              level: this.selectedLevel,
            };

            this.skillService.addSkillToMyProfile(body).subscribe();
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
