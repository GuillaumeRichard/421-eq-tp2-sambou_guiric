import {Component, OnInit} from '@angular/core';
import {PoiService} from "../POI/shared/poi-list-service/poi-list.service";
import {Poi} from "../POI/shared/poi.model";
@Component({
  selector: 'map-component',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
  positions = [];
  selectedPos: Position;
  poiService: PoiService;
  poiList: Poi[] = [
  {id: 1,
    name: 'Cegep Ste-Foy',
    description: 'Le cegep le plus cool',
    address: '2700, ch. Ste-Foy',
    longitude: '46.78589761',
    latitude: '-71.28661394',
    cityId: 1},

  {id: 2,
    name: 'Château Frontenac',
    description: 'L\'icône de la ville de Québec',
    address: '2700, ch. Ste-Foy',
    longitude: '46.810756',
    latitude: '-71.2044479',
    cityId: 1},

  {id: 3,
    name: 'Île Madame',
    description: 'Le lieu où se déroule le meilleur roman \'Les grandes marées\'',
    address: '2700, ch. Ste-Foy',
    longitude: '46.971245',
    latitude: '-70.7868957',
    cityId: 1},
];

  constructor(poiService: PoiService){
    this.poiService = poiService;
  }

  ngOnInit(): void {
    this.getPoiPositions()
  }

  getPoiPositions() {
    for (var i = 0, length = this.poiList.length; i < length; i++)
    {
      var poi = this.poiList[i];
      this.positions.push([poi.longitude, poi.latitude])
    }
  }

  onSelect(pos) {
    this.selectedPos = pos;
  }

}
