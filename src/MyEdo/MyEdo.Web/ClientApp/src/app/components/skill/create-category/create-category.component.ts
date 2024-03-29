import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnInit {

  form: FormGroup;
  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router) { }

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
    })
  }

  createCategory() {
    const body = {
      id: '',
      name: this.form.value['name'],
    };

    this.categoryService.createCategory(body)
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