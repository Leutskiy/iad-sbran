import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AlienOrganizationInvitationComponent } from './alien-organization-invitation.component';

describe('AlienOrganizationInvitationComponent', () => {
  let component: AlienOrganizationInvitationComponent;
  let fixture: ComponentFixture<AlienOrganizationInvitationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlienOrganizationInvitationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlienOrganizationInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
