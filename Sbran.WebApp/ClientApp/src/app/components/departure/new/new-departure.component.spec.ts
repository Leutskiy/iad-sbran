import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewDepartureComponent } from './new-departure.component';

describe('NewDepartureComponent', () => {
  let component: NewDepartureComponent;
  let fixture: ComponentFixture<NewDepartureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewDepartureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewDepartureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
