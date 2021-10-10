import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TrainingModel } from '../../../core/models/training-model';
import { TrainingService } from '../../../core/services/training.service';

@Component({
  selector: 'app-my-trainings',
  templateUrl: './my-trainings.component.html',
  styleUrls: ['./my-trainings.component.css']
})
export class MyTrainingsComponent implements OnInit {

  public trainings$: Observable<TrainingModel[]>;
  constructor(private trainingService: TrainingService) { }

  ngOnInit() {
    this.trainings$ = this.trainingService.getMyTrainings();
  }
}
