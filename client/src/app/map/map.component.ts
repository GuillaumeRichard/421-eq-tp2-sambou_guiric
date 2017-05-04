import {Component, OnInit, Input} from '@angular/core';
import {PoiService} from "../POI/shared/poi-list-service/poi-list.service";
import {Poi} from "../POI/shared/poi.model";
@Component({
  selector: 'map-component',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
  //Québec
  DEFAULT_CITY_ID = 1;

  positions = [];
  poiService: PoiService;
  poiList: Poi[];
  deselectedMarker = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
  selectedMarker = 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png';

  @Input()
  selectedPoi: Poi;

  constructor(poiService: PoiService){
    this.poiService = poiService;
  }

  ngOnInit(): void {
    this.getPoiList(this.DEFAULT_CITY_ID).then(() => this.getPoiPositions());
  }

  getPoiList(id: number): Promise<Poi[]> {
    return this.poiService.getPoiList(id).then(poiList => this.poiList = poiList);
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

  showInfoWindow(event): void {
    let marker = event.target;
    let clickedPoi = this.getPoiByPosition(marker);
    marker.nguiMapComponent.openInfoWindow('iw', marker, {
      nom: clickedPoi.name
    });
  }

  getPoiByPosition(marker): Poi {
    let markerLongitude = MapComponent.roundPosition(marker.getPosition().lat());
    let markerLatitude = MapComponent.roundPosition(marker.getPosition().lng());
    let poiToReturn: Poi;
    for (let i = 0, length = this.poiList.length; i < length; i++)
    {
      let poi = this.poiList[i];
      let poiLongitude = MapComponent.roundPosition(Number(poi.longitude));
      let poiLatitude = MapComponent.roundPosition(Number(poi.latitude));
      if(markerLongitude == poiLongitude && markerLatitude == poiLatitude) {
        poiToReturn = poi;
      }
    }

    return poiToReturn;
  }

  //Cette méthode arrondit la position à 6 chiffres après la virgule
  //Pour que la comparaison entre la position du marqueur et du poi se fasse.
  static roundPosition(position: number): number {
    // Math.round n'arrondit qu'à des nombres entiers.
    // Donc, on multiplie la position par 1000000 pour arrondir les 6 chiffres après la virgule,
    // puis on redivise par 1000000.
    return Math.round(position * 1000000) / 1000000;
  }


}
