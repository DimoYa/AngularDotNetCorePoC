import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EditCategoryModel } from '../../../core/models/edit-category-model';
import { CategoryService } from '../../../core/services/category.service';

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
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.editForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
    });

    this.id = this.route.snapshot.params['id'];
    this.categoryService.getAllCategories()
      .subscribe((data) => {
        this.category = data.filter(c=> c.id == this.id)[0];
      });
  }

  editCategory() {
    const body = {
      id: this.id,
      name: this.editForm.value['name'],
    };

    this.categoryService.editCategory(body)
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
