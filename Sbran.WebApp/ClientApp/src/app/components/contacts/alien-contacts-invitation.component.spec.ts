import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AlienContactsInvitationComponent } from './alien-contacts-invitation.component';

describe('AlienContactsInvitationComponent', () => {
  let component: AlienContactsInvitationComponent;
  let fixture: ComponentFixture<AlienContactsInvitationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlienContactsInvitationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlienContactsInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
