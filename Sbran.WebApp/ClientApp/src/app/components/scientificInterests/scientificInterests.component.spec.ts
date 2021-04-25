import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ScientificInterestsComponent } from './scientificInterests.component';

describe('ScientificInterestsComponent', () => {
  let component: ScientificInterestsComponent;
  let fixture: ComponentFixture<ScientificInterestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ScientificInterestsComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScientificInterestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
