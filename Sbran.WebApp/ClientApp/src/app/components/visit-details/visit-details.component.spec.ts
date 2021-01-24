import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { VisitDetailsInvitationComponent } from './visit-details.component';

describe('VisitDetailsInvitationComponent', () => {
  let component: VisitDetailsInvitationComponent;
  let fixture: ComponentFixture<VisitDetailsInvitationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VisitDetailsInvitationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VisitDetailsInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
