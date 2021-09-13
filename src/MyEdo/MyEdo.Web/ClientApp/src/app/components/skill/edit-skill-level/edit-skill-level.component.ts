import { Component, OnInit } from "@angular/core";
import { DialogBelonging } from "@costlydeveloper/ngx-awesome-popup";
import { Subscription } from "rxjs";
import { SkillService } from "../../../core/services/skill.service";

@Component({
  selector: "app-edit-skill-level",
  templateUrl: "./edit-skill-level.component.html",
  styleUrls: ["./edit-skill-level.component.css"],
})
export class EditSkillLevelComponent implements OnInit {
  private subscriptions: Subscription = new Subscription();
  public data: number[] = [1, 2, 3, 4, 5];

  constructor(
    private skillService: SkillService,
    public dialogBelonging: DialogBelonging
  ) {}

  ngOnInit() {
    this.subscriptions.add(
      this.dialogBelonging.EventsController.onButtonClick$.subscribe(
        (_Button) => {
          if (_Button.ID === "submit") {
            const body = {
              skillId: this.dialogBelonging.CustomData.id,
              skillName: this.dialogBelonging.CustomData.name,
              level: Number(this.dialogBelonging.CustomData.level),
            };

            this.skillService.editSkillLevel(body).subscribe();
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