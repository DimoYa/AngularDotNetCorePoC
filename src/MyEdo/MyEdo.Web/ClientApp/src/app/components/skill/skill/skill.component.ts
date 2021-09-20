import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { SkillCategoryModel } from "../../../core/models/skill-model";
import { SkillModel } from "../../../core/models/skill-model";
import { SkillService } from "../../../core/services/skill.service";
import { Router } from "@angular/router";
import {
  ButtonLayoutDisplay,
  ButtonMaker,
  ConfirmBoxInitializer,
  DialogInitializer,
} from "@costlydeveloper/ngx-awesome-popup";
import { AddSkillComponent } from "../add-skill/add-skill.component";
import { AuthorizeService } from "../../../../../src/api-authorization/authorize.service";
import { Observable } from "rxjs";
import { AddSkillModel } from "../../../core/models/add-skill-model";
import { EditSkillLevelComponent } from "../edit-skill-level/edit-skill-level.component";

@Component({
  selector: "app-skill",
  templateUrl: "./skill.component.html",
  styleUrls: ["./skill.component.css"],
})
export class SkillComponent implements OnInit {
  clickButton: boolean = false;
  confirmMsg = "Are you sure that you want to";

  @Input("skill")
  public skill: SkillCategoryModel;
  public skillCategories: SkillCategoryModel[];
  public mySkills: SkillCategoryModel[];
  public isAdminView: boolean;
  public isResource: Observable<boolean>;

  @Output()
  emiter = new EventEmitter();

  constructor(
    private skillService: SkillService,
    private authorizeService: AuthorizeService,
    private router: Router
  ) {}

  ngOnInit() {
    this.skillService
      .getAllSkills()
      .subscribe((data) => (this.skillCategories = data));

    this.skillService.getMySkills().subscribe((data) => (this.mySkills = data));
    this.isAdminView = this.router.url.includes("/all-skills");
    this.isResource = this.authorizeService.isResource();
  }

  public isPossibleToAddSkill(skillId: string): boolean {
    let merged: string[] = [].concat
      .apply(
        [],
        this.mySkills.map((s) => s.skills)
      )
      .map((s) => s.skillId);
    return !merged.some((x) => x == skillId);
  }

  public editCategory(categoryId: string) {
    this.clickButton = true;
    console.log(categoryId);
  }

  public deleteCategory(categoryId: string) {
    this.clickButton = true;

    const currentCategory = this.GetCurrentCategory(categoryId);
    const confirmBox = new ConfirmBoxInitializer();
    confirmBox.setTitle(
      `${this.confirmMsg} delete category: ${currentCategory.categoryName}?`
    );
    confirmBox.setButtonLabels("YES", "NO");

    const subscription = confirmBox.openConfirmBox$().subscribe((resp) => {
      if (resp.Success) {
        const body = {
          id: currentCategory.categoryId,
          name: currentCategory.categoryName,
        };

        this.skillService.deleteCategory(body).subscribe(() => {
          this.emiter.emit(null);
        });
      }
      subscription.unsubscribe();
    });
  }

  public deleteSkill(categoryId: string, skillId: string) {
    const currentSkill = this.GetCurrentSkill(categoryId, skillId);
    const confirmBox = new ConfirmBoxInitializer();
    confirmBox.setTitle(
      `${this.confirmMsg} delete skill: ${currentSkill.name}?`
    );
    confirmBox.setButtonLabels("YES", "NO");

    const subscription = confirmBox.openConfirmBox$().subscribe((resp) => {
      if (resp.Success) {
        const body = {
          id: currentSkill.id,
          name: currentSkill.name,
        };

        this.skillService.deleteSkill(body).subscribe(() => {
          this.emiter.emit(null);
        });
      }
      subscription.unsubscribe();
    });
  }

  public addSkillToMyProfile(categoryId: string, skillId: string) {
    const dialogPopup = new DialogInitializer(AddSkillComponent);
    const currentSkill = this.GetCurrentSkill(categoryId, skillId);
    dialogPopup.setCustomData({ id: skillId, name: currentSkill.name });

    dialogPopup.setButtons([
      new ButtonMaker("Submit", "submit", ButtonLayoutDisplay.SUCCESS),
      new ButtonMaker("Cancel", "cancel", ButtonLayoutDisplay.SECONDARY),
    ]);

    const subscription = dialogPopup.openDialog$().subscribe(() => {
      this.emiter.emit(null);
      subscription.unsubscribe();
    });
  }

  public removeSkillFromMyProfile(skill: AddSkillModel) {
    const body = {
      id: skill.skillId,
      name: skill.skillName,
    };
    this.skillService.removeSkillFromMyProfile(body).subscribe(() => {
      this.emiter.emit(null);
    });
  }

  public editSkillLevel(skill: AddSkillModel) {
    const dialogPopup = new DialogInitializer(EditSkillLevelComponent);

    dialogPopup.setCustomData({ id: skill.skillId, name: skill.skillName, level: skill.skillLevel });

    dialogPopup.setButtons([
      new ButtonMaker("Submit", "submit", ButtonLayoutDisplay.SUCCESS),
      new ButtonMaker("Cancel", "cancel", ButtonLayoutDisplay.SECONDARY),
    ]);

    const subscription = dialogPopup.openDialog$().subscribe(() => {
      this.emiter.emit(null);
      subscription.unsubscribe();
    });
  }

  private GetCurrentCategory(categoryId: string): SkillCategoryModel {
    return this.skillCategories.filter((c) => c.categoryId == categoryId)[0];
  }

  private GetCurrentSkill(categoryId: string, skillId: string): SkillModel {
    return this.skillCategories
      .filter((c) => c.categoryId == categoryId)[0]
      .skills.filter((s) => s.id == skillId)[0];
  }
}
