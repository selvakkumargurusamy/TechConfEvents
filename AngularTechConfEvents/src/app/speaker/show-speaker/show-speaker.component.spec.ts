import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowSpeakerComponent } from './show-speaker.component';

describe('ShowSpeakerComponent', () => {
  let component: ShowSpeakerComponent;
  let fixture: ComponentFixture<ShowSpeakerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowSpeakerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowSpeakerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
