import { TestBed } from '@angular/core/testing';

import { StateRegistrationDataService } from './state-registration-data.service';

describe('StateRegistrationDataService', () => {
  let service: StateRegistrationDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StateRegistrationDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
