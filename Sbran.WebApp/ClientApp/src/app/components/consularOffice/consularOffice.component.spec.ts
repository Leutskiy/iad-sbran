import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ConsularOfficeComponent } from './consularOffice.component';

describe('ConsularOfficeComponent', () => {
  let component: ConsularOfficeComponent;
  let fixture: ComponentFixture<ConsularOfficeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConsularOfficeComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsularOfficeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
