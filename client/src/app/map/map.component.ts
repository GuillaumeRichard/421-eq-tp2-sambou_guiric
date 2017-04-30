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
  selectedPos: Position;
  poiService: PoiService;
  poiList: Poi[];

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

  onClick(event): void {
    let marker = event.target;
    let clickedPoi = this.getPoiByPosition(marker);
    this.showPoiDetail(marker, clickedPoi);
  }

  getPoiByPosition(marker): Poi {
    let markerLongitude = marker.getPosition().lat();
    let markerLatitude = marker.getPosition().lng();
    let poiToReturn: Poi;
    for (let i = 0, length = this.poiList.length; i < length; i++)
    {
      let poi = this.poiList[i];
      if(markerLongitude == poi.longitude && markerLatitude == poi.latitude) {
        poiToReturn = poi;
      }
    }

    return poiToReturn;
  }

  showPoiDetail(marker, clickedPoi): void {
    marker.nguiMapComponent.openInfoWindow('iw', marker, {
      nom: clickedPoi.name
    });
  }

  onSelect(pos): void {
    this.selectedPos = pos;
  }

}
