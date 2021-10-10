import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import {
  TrainingModel,
  TrainingStatus,
  TrainingType,
} from "../../../core/models/training-model";
import { TrainingService } from "../../../core/services/training.service";
import { DateAdapter } from "@angular/material";

@Component({
  selector: "app-edit-training",
  templateUrl: "./edit-training.component.html",
  styleUrls: ["./edit-training.component.css"],
})
export class EditTrainingComponent implements OnInit {
  editForm: FormGroup;
  training: TrainingModel;
  id: string;
  types: string[] = [
    TrainingType[TrainingType.Optional],
    TrainingType[TrainingType.Mandatory],
  ];
  statuses: string[] = [
    TrainingStatus[TrainingStatus.Active],
    TrainingStatus[TrainingStatus.Inactive],
  ];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private trainingService: TrainingService,
    private dateAdapter: DateAdapter<Date>
  ) {
    this.dateAdapter.setLocale("your locale");
  }

  ngOnInit() {
    this.editForm = this.fb.group({
      name: ["", [Validators.required, Validators.minLength(2)]],
      type: ["", [Validators.required]],
      status: ["", [Validators.required]],
      dueDate: ["", [Validators.required]],
    });

    this.id = this.route.snapshot.params["id"];
    this.trainingService.getAllTrainings().subscribe((data) => {
      this.training = data.filter((c) => c.id == this.id)[0];
      this.editForm.controls["name"].setValue(this.training.name, {
        onlySelf: true,
      });
      this.editForm.controls["type"].setValue(
        TrainingType[this.training.type],
        {
          onlySelf: true,
        }
      );
      this.editForm.controls["status"].setValue(
        TrainingStatus[this.training.status],
        { onlySelf: true }
      );
      this.editForm.controls["dueDate"].setValue(this.training.dueDate, {
        onlySelf: true,
      });
    });
  }

  editTraining() {
    const body = {
      id: this.id,
      name: this.editForm.value["name"],
      type: TrainingType[this.editForm.value["type"]],
      status: TrainingStatus[this.editForm.value["status"]],
      dueDate: this.editForm.value["dueDate"],
    };

    this.trainingService.editTraining(body)
      .subscribe(() => {
        this.router.navigate(['/all-trainings']);
      })
  }
  get f() {
    return this.editForm.controls;
  }

  get invalid() {
    return this.editForm.invalid;
  }
}
