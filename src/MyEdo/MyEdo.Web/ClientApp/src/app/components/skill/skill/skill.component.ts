import { Component, Input, OnInit } from '@angular/core';
import SkillCategoryModel from "../../../core/models/skill-model";

@Component({
  selector: 'app-skill',
  templateUrl: './skill.component.html',
  styleUrls: ['./skill.component.css']
})
export class SkillComponent implements OnInit {

  @Input('skill')
  skill: SkillCategoryModel;
  constructor() { }

  ngOnInit() {
  }

}
