import { TestBed, inject } from '@angular/core/testing';

import { PoiListService } from './poi-list.service';

describe('PoiListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PoiListService]
    });
  });

  it('should ...', inject([PoiListService], (service: PoiListService) => {
    expect(service).toBeTruthy();
  }));
});
