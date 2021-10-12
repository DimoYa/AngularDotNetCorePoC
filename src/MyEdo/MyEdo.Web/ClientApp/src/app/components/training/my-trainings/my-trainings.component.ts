import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { TrainingModel } from '../../../core/models/training-model';
import { TrainingService } from '../../../core/services/training.service';

@Component({
  selector: 'app-my-trainings',
  templateUrl: './my-trainings.component.html',
  styleUrls: ['./my-trainings.component.css']
})
export class MyTrainingsComponent implements OnInit {

  public trainings: TrainingModel[];
  public pageSlice: TrainingModel[];
  constructor(private trainingService: TrainingService) { }

  ngOnInit() {
    this.trainingService.getMyTrainings().subscribe((data=> {
      this.trainings = data;
      this.pageSlice = this.trainings.slice(0, 5);
    }));
  }

  public OnPageChange(event: PageEvent) {
    const startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize;
    if (endIndex > this.trainings.length) {
      endIndex = this.trainings.length;
    }
    this.pageSlice = this.trainings.slice(startIndex, endIndex);
  }
}
