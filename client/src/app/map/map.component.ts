import {Component, OnInit} from '@angular/core';
import {PoiService} from "../POI/shared/poi-list-service/poi-list.service";
import {Poi} from "../POI/shared/poi.model";
@Component({
  selector: 'map-component',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
  DEFAULT_CITY = "Quebec";

  positions = [];
  poiService: PoiService;
  poiList: Poi[];
  deselectedMarker = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
  selectedMarker = 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png';

  constructor(poiService: PoiService){
    this.poiService = poiService;
  }

  ngOnInit(): void {
    this.getPoiList(this.DEFAULT_CITY).then(() => this.getPoiPositions());
  }

  getPoiList(name: string): Promise<Poi[]> {
    return this.poiService.getPoiList(name).then(poiList => this.poiList = poiList);
  }

  getPoiPositions(): void {
    for (let i = 0, length = this.poiList.length; i < length; i++)
    {
      let poi = this.poiList[i];
      this.positions.push([poi.longitude, poi.latitude])
    }
  }

  selectMarker(event): void {
    let marker = event.target;
    marker.setIcon(this.selectedMarker);
  }

  deselectMarker(event): void {
    let marker = event.target;
    marker.setIcon(this.deselectedMarker);
  }

  // onClick(event): void {
  //   let marker = event.target;
  //   let clickedPoi = this.getPoiByPosition(marker);
  //   this.showPoiDetail(marker, clickedPoi);
  // }
  //
  // getPoiByPosition(marker): Poi {
  //   let markerLongitude = marker.getPosition().lat();
  //   let markerLatitude = marker.getPosition().lng();
  //   let poiToReturn: Poi;
  //   for (let i = 0, length = this.poiList.length; i < length; i++)
  //   {
  //     let poi = this.poiList[i];
  //     if(markerLongitude == poi.longitude && markerLatitude == poi.latitude) {
  //       poiToReturn = poi;
  //     }
  //   }
  //
  //   return poiToReturn;
  // }
  //
  // showPoiDetail(marker, clickedPoi): void {
  //   marker.nguiMapComponent.openInfoWindow('iw', marker, {
  //     nom: clickedPoi.name
  //   });
  // }

}
