import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditSpeakerComponent } from './add-edit-speaker.component';

describe('AddEditSpeakerComponent', () => {
  let component: AddEditSpeakerComponent;
  let fixture: ComponentFixture<AddEditSpeakerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditSpeakerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditSpeakerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
