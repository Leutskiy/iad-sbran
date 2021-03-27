import { TestBed } from '@angular/core/testing';

import { ScientificInterestsDataService } from './scientificInterests-data.service';

describe('ScientificInterestsDataService', () => {
  let service: ScientificInterestsDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScientificInterestsDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
