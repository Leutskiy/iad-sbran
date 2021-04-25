import { TestBed } from '@angular/core/testing';

import { PassportDataService } from './passport-data.service';

describe('PassportDataService', () => {
  let service: PassportDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PassportDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
