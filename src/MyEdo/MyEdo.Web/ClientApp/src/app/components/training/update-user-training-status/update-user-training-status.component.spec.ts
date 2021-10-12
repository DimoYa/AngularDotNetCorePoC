import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateUserTrainingStatusComponent } from './update-user-training-status.component';

describe('UpdateUserTrainingStatusComponent', () => {
  let component: UpdateUserTrainingStatusComponent;
  let fixture: ComponentFixture<UpdateUserTrainingStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateUserTrainingStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateUserTrainingStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
