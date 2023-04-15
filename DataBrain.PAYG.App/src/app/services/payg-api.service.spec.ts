import { TestBed } from '@angular/core/testing';

import { PaygApiService } from './payg-api.service';

describe('PaygApiService', () => {
  let service: PaygApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PaygApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
