import { TestBed } from '@angular/core/testing';

import { NewrequestInterceptor } from './newrequest.interceptor';

describe('NewrequestInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      NewrequestInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: NewrequestInterceptor = TestBed.inject(NewrequestInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
