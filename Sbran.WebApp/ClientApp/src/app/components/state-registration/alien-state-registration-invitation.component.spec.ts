import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AlienStateRegistrationInvitationComponent } from './alien-state-registration-invitation.component';

describe('AlienStateRegistrationInvitationComponent', () => {
  let component: AlienStateRegistrationInvitationComponent;
  let fixture: ComponentFixture<AlienStateRegistrationInvitationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlienStateRegistrationInvitationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlienStateRegistrationInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
