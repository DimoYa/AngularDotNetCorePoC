import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { SkillCategoryModel } from "../../../core/models/skill-model";
import { SkillService } from "../../../core/services/skill.service";

@Component({
  selector: "app-my-skills",
  templateUrl: "./my-skills.component.html",
  styleUrls: ["./my-skills.component.css"],
})
export class MySkillsComponent implements OnInit {
  skills$: Observable<SkillCategoryModel[]>;

  constructor(private skillService: SkillService) {}

  ngOnInit() {
    this.skills$ = this.skillService.getMySkills();
  }

  public fromChild() {
    this.skills$ = this.skillService.getMySkills();
  }
}
