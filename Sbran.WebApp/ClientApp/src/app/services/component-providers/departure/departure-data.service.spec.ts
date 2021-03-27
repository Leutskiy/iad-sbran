import { TestBed } from '@angular/core/testing';

import { DepartureDataService } from './departure-data.service';

describe('DepartureDataService', () => {
  let service: DepartureDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DepartureDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
