import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import {SkillCategoryModel} from "../../../core/models/skill-model";
import { SkillService } from "../../../core/services/skill.service";

@Component({
  selector: "app-all-skills",
  templateUrl: "./all-skills.component.html",
  styleUrls: ["./all-skills.component.css"],
})
export class AllSkillsComponent implements OnInit {
  skills$: Observable<SkillCategoryModel[]>;

  constructor(private skillService : SkillService) {}

  ngOnInit() {
    this.skills$ =  this.skillService.getAllSkills();
  }

  public fromChild() {
    this.skills$ =  this.skillService.getAllSkills();
  }
}
