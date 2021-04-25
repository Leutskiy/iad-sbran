import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AlienPassportInvitationComponent } from './alien-passport-invitation.component';

describe('AlienPassportInvitationComponent', () => {
  let component: AlienPassportInvitationComponent;
  let fixture: ComponentFixture<AlienPassportInvitationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlienPassportInvitationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlienPassportInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
