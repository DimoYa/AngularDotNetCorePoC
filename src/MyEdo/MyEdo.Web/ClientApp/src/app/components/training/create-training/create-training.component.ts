import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import {
  TrainingStatus,
  TrainingType,
} from "../../../core/models/training-model";
import { DateAdapter } from '@angular/material';
import { TrainingService } from "../../../core/services/training.service";

@Component({
  selector: "app-create-training",
  templateUrl: "./create-training.component.html",
  styleUrls: ["./create-training.component.css"],
})
export class CreateTrainingComponent implements OnInit {
  form: FormGroup;
  types = Object.values(TrainingType).filter(value => typeof value !== 'number');
  statuses = Object.values(TrainingStatus).filter(value => typeof value !== 'number');
 
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private trainingService: TrainingService,
    private dateAdapter: DateAdapter<Date>
  ) {
    this.dateAdapter.setLocale('your locale'); 
  }

  ngOnInit() {
    this.form = this.fb.group({
      name: ["", [Validators.required, Validators.minLength(2)]],
      type: ["", [Validators.required]],
      status: ["", [Validators.required]],
      dueDate: ["", [Validators.required]],
    });

    this.form.controls["type"].setValue(TrainingType[TrainingType.Optional], {
      onlySelf: true,
    });
    this.form.controls["status"].setValue(
      TrainingStatus[TrainingStatus.Active],
      { onlySelf: true }
    );
  }

  createTraining() {

    const body = {
      id: "1",
      name: this.form.value["name"],
      type: TrainingType[this.form.value["type"]],
      status: TrainingStatus[this.form.value["status"]],
      dueDate: this.form.value["dueDate"],
    };

    this.trainingService.createTraining(body).subscribe(() => {
      this.router.navigate(["/all-trainings"]);
    });
  }
  get f() {
    return this.form.controls;
  }

  get invalid() {
    return this.form.invalid;
  }
}
