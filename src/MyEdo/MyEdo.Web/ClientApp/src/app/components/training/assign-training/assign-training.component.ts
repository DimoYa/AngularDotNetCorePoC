import { Component, OnInit } from "@angular/core";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { TrainingModel } from "../../../core/models/training-model";
import UserModel from "../../../core/models/user-model";
import { UserTrainingModel } from "../../../core/models/user-trainings-model";
import { AdminService } from "../../../core/services/admin.service";
import { TrainingService } from "../../../core/services/training.service";

@Component({
  selector: "app-assign-training",
  templateUrl: "./assign-training.component.html",
  styleUrls: ["./assign-training.component.css"],
})
export class AssignTrainingComponent implements OnInit {
  form: FormGroup;
  training: TrainingModel;
  id: string;
  resources: UserModel[];
  userTrainings: UserTrainingModel[];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private trainingService: TrainingService,
    private adminService: AdminService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      training: ["", [Validators.required]],
      resource: ["", [Validators.required]],
    });

    this.trainingService.getAllUsersTrainings().subscribe((data) => {
      this.userTrainings = data;
    });

    this.adminService.getAllUsers().subscribe((data) => {
      let filtered = this.userTrainings
        .filter((t) => t.trainings.some((t) => t.trainingId == this.id))
        .map((t) => t.userId);

      this.resources = data
        .filter((x) => !filtered.includes(x.id))
        .sort((a, b) => a.fullName.localeCompare(b.fullName));
    });

    this.id = this.route.snapshot.params["id"];
    this.trainingService.getAllTrainings().subscribe((data) => {
      this.training = data.filter((c) => c.id == this.id)[0];
      this.form.controls["training"].setValue(this.training.name, {
        onlySelf: true,
      });
    });
  }

  assignTraining() {
    const body = {
      trainingId: this.id,
      userId: this.form.value["resource"].id,
      status: 1,
    };

    this.trainingService.assignTraining(body).subscribe(() => {
      this.router.navigate(["/user-trainings"]);
    });
  }
  get f() {
    return this.form.controls;
  }

  get invalid() {
    return this.form.invalid;
  }
}
