import {Component, OnInit} from '@angular/core';
import {PoiListService} from "../POI/shared/poi-list-service/poi-list.service";
import {Poi} from "../POI/shared/poi.model";
@Component({
  selector: 'map-component',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
  positions = [];
  selectedPos: Position;
  poiService: PoiListService;
  poiList: Poi[] = [
  {Id: 1,
    Name: 'Cegep Ste-Foy',
    Description: 'Le cegep le plus cool',
    Address: '2700, ch. Ste-Foy',
    Longitude: '46.78589761',
    Latitude: '-71.28661394',
    CityId: 1},

  {Id: 2,
    Name: 'Château Frontenac',
    Description: 'L\'icône de la ville de Québec',
    Address: '2700, ch. Ste-Foy',
    Longitude: '46.810756',
    Latitude: '-71.2044479',
    CityId: 1},

  {Id: 3,
    Name: 'Île Madame',
    Description: 'Le lieu où se déroule le meilleur roman \'Les grandes marées\'',
    Address: '2700, ch. Ste-Foy',
    Longitude: '46.971245',
    Latitude: '-70.7868957',
    CityId: 1},
];

  constructor(poiService: PoiListService){
    this.poiService = poiService;
  }

  ngOnInit(): void {
    this.getPoiPositions()
  }

  getPoiPositions() {
    for (var i = 0, length = this.poiList.length; i < length; i++)
    {
      var poi = this.poiList[i];
      this.positions.push([poi.Longitude, poi.Latitude])
    }
  }

  onSelect(pos) {
    this.selectedPos = pos;
  }

}
