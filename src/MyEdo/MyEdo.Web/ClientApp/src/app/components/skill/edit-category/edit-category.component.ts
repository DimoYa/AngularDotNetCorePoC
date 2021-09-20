import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EditCategoryModel } from '../../../core/models/edit-category-model';
import { SkillService } from '../../../core/services/skill.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {

 
  editForm: FormGroup;
  category: EditCategoryModel;
  id: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private skillService: SkillService
  ) { }

  ngOnInit() {
    this.editForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
    });

    this.id = this.route.snapshot.params['id'];
    this.skillService.getAllCategories()
      .subscribe((data) => {
        this.category = data.filter(c=> c.id == this.id)[0];
      });
  }

  editCategory() {
    const body = {
      id: this.id,
      name: this.editForm.value['name'],
    };

    this.skillService.editCategory(body)
      .subscribe(() => {
        this.router.navigate(['/all-skills']);
      })
  }
  get f() {
    return this.editForm.controls;
  }

  get invalid() {
    return this.editForm.invalid;
  }
}
