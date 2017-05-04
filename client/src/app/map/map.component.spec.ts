import {fakeAsync, tick} from '@angular/core/testing';

import { MapComponent } from './map.component';

describe('MapComponent', () => {
  let mapComponent: MapComponent;

  let poiList = [
  {
    id: 1,
    name: "Cegep Ste-Foy",
    description: "Le cegep le plus cool",
    address: "2700, ch. Ste-Foy",
    longitude: "46.78589761",
    latitude: "-71.28661394",
    cityId: 1
  },

  {
    id: 1,
      name: "Cegep Ste-Foy1",
    description: "Le cegep le plus cool",
    address: "2700, ch. Ste-Foy",
    longitude: "46.78589761",
    latitude: "-71.28661394",
    cityId: 1
  },

  {
    id: 1,
      name: "Cegep Ste-Foy2",
    description: "Le cegep le plus cool",
    address: "2700, ch. Ste-Foy",
    longitude: "46.78589761",
    latitude: "-71.28661394",
    cityId: 1
  }
  ];

  describe('ngOnInit', () => {
    it('ngOnInit should associate services points of interest to own points of interest list', fakeAsync(() => {
      let  mockPoiListService = jasmine.createSpyObj('PoiListService', ["getPoiList"]);
      mockPoiListService.getPoiList.and.returnValue(Promise.resolve(poiList));
      mapComponent = new MapComponent(mockPoiListService);

      mapComponent.ngOnInit();
      tick();

      expect(mapComponent.poiList).toEqual(poiList);
    }));
  });


});
