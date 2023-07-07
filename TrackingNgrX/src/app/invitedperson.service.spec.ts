import { TestBed } from '@angular/core/testing';

import { InvitedpersonService } from './invitedperson.service';

describe('InvitedpersonService', () => {
  let service: InvitedpersonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InvitedpersonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
