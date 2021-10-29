import { Component, Inject, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { DialogBelonging } from "@costlydeveloper/ngx-awesome-popup";
import { Subscription } from "rxjs";
import { SkillLevel } from "../../../core/models/add-skill-model";
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
    private router: Router,
    @Inject('dialogBelonging') public dialogBelonging: DialogBelonging
  ) {}

  data = Object.values(SkillLevel).filter(value => typeof value !== 'number');
  selectedValue = this.data[0];

  ngOnInit() {

    this.subscriptions.add(
      this.dialogBelonging.EventsController.onButtonClick$.subscribe(
        (_Button) => {
          if (_Button.ID === "submit") {
            const body = {
              skillId: this.dialogBelonging.CustomData.id,
              skillName: this.dialogBelonging.CustomData.name,
              level: this.selectedValue,
            };

            this.skillService.addSkillToMyProfile(body).subscribe(() => {
              this.router.navigate(["/my-skills"]);
            });
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
