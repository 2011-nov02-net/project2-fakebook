import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilePictureViewComponent } from './profile-picture-view.component';

describe('ProfilePictureViewComponent', () => {
  let component: ProfilePictureViewComponent;
  let fixture: ComponentFixture<ProfilePictureViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfilePictureViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfilePictureViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
