import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { InternationalAgreementComponent } from './internationalAgreement.component';

describe('InternationalAgreementComponent', () => {
  let component: InternationalAgreementComponent;
  let fixture: ComponentFixture<InternationalAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [InternationalAgreementComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InternationalAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
