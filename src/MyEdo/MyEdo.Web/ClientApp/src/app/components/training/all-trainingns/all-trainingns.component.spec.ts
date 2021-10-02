import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllTrainingnsComponent } from './all-trainingns.component';

describe('AllTrainingnsComponent', () => {
  let component: AllTrainingnsComponent;
  let fixture: ComponentFixture<AllTrainingnsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllTrainingnsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllTrainingnsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
