import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { EditCategoryModel } from "../../../core/models/edit-category-model";
import { CategoryService } from "../../../core/services/category.service";
import { SkillService } from "../../../core/services/skill.service";

@Component({
  selector: "app-create-skill",
  templateUrl: "./create-skill.component.html",
  styleUrls: ["./create-skill.component.css"],
})
export class CreateSkillComponent implements OnInit {
  skillCategories: EditCategoryModel[];
  selectedValue: EditCategoryModel;

  form: FormGroup;
  constructor(
    private categoryService: CategoryService,
    private skillService: SkillService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      skillName: ["", [Validators.required, Validators.minLength(2)]],
      category: ["", [Validators.required]],
    });

    this.categoryService.getAllCategories().subscribe((data) => {
      this.skillCategories = data;
      this.selectedValue = data[0];
    });
  }

  createSkill() {
    const body = {
      id: "",
      name: this.form.value['skillName'],
      skillCategoryId: this.form.value['category']['id'],
      skillCategoryName: this.form.value['category']['name'],
    };

    this.skillService.createSkill(body)
      .subscribe(() => {
        this.router.navigate(['/all-skills']);
      })
  }
  get f() {
    return this.form.controls;
  }

  get invalid() {
    return this.form.invalid;
  }
}
