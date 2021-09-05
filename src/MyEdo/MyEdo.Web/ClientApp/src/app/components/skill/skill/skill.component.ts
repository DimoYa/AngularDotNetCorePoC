import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import SkillCategoryModel from "../../../core/models/skill-model";
import { SkillService } from "../../../core/services/skill.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-skill",
  templateUrl: "./skill.component.html",
  styleUrls: ["./skill.component.css"],
})
export class SkillComponent implements OnInit {
  clickButton: boolean = false;
  @Input("skill")
  skill: SkillCategoryModel;
  skillCategories: SkillCategoryModel[];
  
  @Output()
  deleteEvent = new EventEmitter();

  constructor(
    private skillService: SkillService,
    private router: Router
  ) {}

  ngOnInit() {
    this.skillService
      .getAllSkills()
      .subscribe((data) => (this.skillCategories = data));
  }

  public editCategory(categoryId: string) {
    this.clickButton = true;
    console.log(categoryId);
  }

  public deleteCategory(categoryId: string) {
    this.clickButton = true;
    const currentCategory = this.GetCurrentCategory(categoryId);
    const body = {
      id: currentCategory.categoryId,
      name: currentCategory.categoryName,
    };

    this.skillService.deleteCategory(body).subscribe(() => {
      this.deleteEvent.emit(null);
    });
  }

  private GetCurrentCategory(categoryId: string): SkillCategoryModel {
    return this.skillCategories.filter((c) => c.categoryId == categoryId)[0];
  }
}
