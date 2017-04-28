import { Component, OnInit } from '@angular/core';

import { PoiService } from '../shared/poi-list-service/poi-list.service';
import { Router } from '@angular/router';
import { Poi } from '../shared/poi.model';


@Component({
  selector: 'poi-list-component',
  templateUrl: `./poi-list.component.html`,
  styleUrls: [`./poi-list.component.css`]
})

export class PoiListComponent implements OnInit {
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
  selectedPoi: Poi;

  constructor(
    // private router: Router,
    private poiService: PoiService
  ) { }

  getPoiList(name: string): void {
    this.poiService.getPoiList(name).then(poiList => this.poiList = poiList);
  }

  ngOnInit(): void {
    this.getPoiList('');
  }

  onSelect(poi: Poi): void {
    this.selectedPoi = poi;
  }

  gotoDetail(): void {
    // this.router.navigate(['/detail', this.selectedPoi.Id]);
  }

}
