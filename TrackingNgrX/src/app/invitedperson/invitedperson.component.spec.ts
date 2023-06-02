import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitedpersonComponent } from './invitedperson.component';

describe('InvitedpersonComponent', () => {
  let component: InvitedpersonComponent;
  let fixture: ComponentFixture<InvitedpersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvitedpersonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvitedpersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
