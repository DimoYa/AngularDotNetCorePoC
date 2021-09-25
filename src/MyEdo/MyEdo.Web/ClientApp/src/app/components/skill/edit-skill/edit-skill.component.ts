import { Component, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { EditCategoryModel } from "src/app/core/models/edit-category-model";
import { EditSkillModel } from "src/app/core/models/edit-skill-model";
import { CategoryService } from "src/app/core/services/category.service";
import { SkillService } from "src/app/core/services/skill.service";

@Component({
  selector: "app-edit-skill",
  templateUrl: "./edit-skill.component.html",
  styleUrls: ["./edit-skill.component.css"],
})
export class EditSkillComponent implements OnInit {
  @ViewChild("f", { static: true }) form: FormGroup;

  skillCategories: EditCategoryModel[];
  selectedValue: EditSkillModel;
  selectedDropDown: EditCategoryModel;
  categoryId: string;
  id: string;

  constructor(
    private categoryService: CategoryService,
    private skillService: SkillService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      skillName: ["", [Validators.required, Validators.minLength(2)]],
      category: ["", [Validators.required]],
    });

    this.id = this.route.snapshot.params["id"];

    this.skillService.getSkillById(this.id).subscribe((data) => {
      this.selectedValue = data;
      this.categoryId = this.selectedValue.skillCategoryId;
      this.form.controls.skillName.setValue(this.selectedValue.name);
      this.categoryService.getAllCategories().subscribe((data) => {
        this.skillCategories = data;
        this.selectedDropDown = data.filter((s) => s.id === this.categoryId)[0];
        this.form.controls.category.setValue(this.selectedDropDown);
      });
    });
  }

  editSkill() {
    const body = {
      id: this.id,
      name: this.form.value["skillName"],
      skillCategoryId: this.form.value["category"]["id"],
      skillCategoryName: this.form.value["category"]["name"],
    };

    this.skillService.editSkill(body).subscribe(() => {
      this.router.navigate(["/all-skills"]);
    });
  }
  get f() {
    return this.form.controls;
  }

  get invalid() {
    return this.form.invalid;
  }
}
