import { TestBed } from '@angular/core/testing';

import { InternationalAgreementDataService } from './internationalAgreement-data.service';

describe('InternationalAgreementDataService', () => {
  let service: InternationalAgreementDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InternationalAgreementDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
