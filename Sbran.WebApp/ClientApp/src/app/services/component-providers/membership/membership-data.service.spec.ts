import { TestBed } from '@angular/core/testing';

import { MembershipDataService } from './membership-data.service';

describe('MembershipDataService', () => {
  let service: MembershipDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MembershipDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
