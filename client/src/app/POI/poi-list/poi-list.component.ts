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
  DEFAULT_CITY = "Quebec";

  poiList: Poi[];
  selectedPoi: Poi;

  constructor(
    // private router: Router,
    private poiService: PoiService
  ) { }

  getPoiList(name: string): void {
    this.poiService.getPoiList(name).then(poiList => this.poiList = poiList);
  }

  ngOnInit(): void {
    this.getPoiList(this.DEFAULT_CITY);
  }

  onSelect(poi: Poi): void {
    this.selectedPoi = poi;
  }

  gotoDetail(): void {
    // this.router.navigate(['/detail', this.selectedPoi.Id]);
  }

}
