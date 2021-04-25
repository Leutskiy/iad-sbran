import { TestBed } from '@angular/core/testing';

import { ScientificDataService } from './scientific-data.service';

describe('ScientificDataService', () => {
  let service: ScientificDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScientificDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
