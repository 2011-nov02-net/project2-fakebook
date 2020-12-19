import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OktaViewComponent } from './okta-view.component';

describe('OktaViewComponent', () => {
  let component: OktaViewComponent;
  let fixture: ComponentFixture<OktaViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OktaViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OktaViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
