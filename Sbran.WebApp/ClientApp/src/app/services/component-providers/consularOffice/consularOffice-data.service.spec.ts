import { TestBed } from '@angular/core/testing';

import { ConsularOfficeDataService } from './consularOffice-data.service';

describe('ConsularOfficeDataService', () => {
  let service: ConsularOfficeDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConsularOfficeDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
