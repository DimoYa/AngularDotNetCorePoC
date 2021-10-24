import { Component, Inject, OnInit } from "@angular/core";
import { DialogBelonging } from "@costlydeveloper/ngx-awesome-popup";
import { Subscription } from "rxjs";
import { SkillLevel } from "../../../core/models/add-skill-model";
import { SkillService } from "../../../core/services/skill.service";

@Component({
  selector: "app-edit-skill-level",
  templateUrl: "./edit-skill-level.component.html",
  styleUrls: ["./edit-skill-level.component.css"],
})
export class EditSkillLevelComponent implements OnInit {
  private subscriptions: Subscription = new Subscription();
  public data = Object.values(SkillLevel).filter(value => typeof value !== 'number');
  public choosen : string

  constructor(
    private skillService: SkillService,
    @Inject('dialogBelonging') private dialogBelonging: DialogBelonging
  ) {}

  ngOnInit() {
    this.choosen = SkillLevel[this.dialogBelonging.CustomData.level]
    this.subscriptions.add(
      this.dialogBelonging.EventsController.onButtonClick$.subscribe(
        (_Button) => {
          if (_Button.ID === "submit") {
            const body = {
              skillId: this.dialogBelonging.CustomData.id,
              skillName: this.dialogBelonging.CustomData.name,
              level: this.choosen
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
