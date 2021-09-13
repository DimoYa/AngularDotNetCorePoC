import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSkillLevelComponent } from './edit-skill-level.component';

describe('EditSkillLevelComponent', () => {
  let component: EditSkillLevelComponent;
  let fixture: ComponentFixture<EditSkillLevelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditSkillLevelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditSkillLevelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
