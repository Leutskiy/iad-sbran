import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeStateRegistrationComponent } from './employee-state-registration.component';

describe('EmployeeStateRegistrationComponent', () => {
  let component: EmployeeStateRegistrationComponent;
  let fixture: ComponentFixture<EmployeeStateRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeeStateRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeStateRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
