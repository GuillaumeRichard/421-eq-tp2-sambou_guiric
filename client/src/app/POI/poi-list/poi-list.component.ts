import { Component, OnInit, EventEmitter, Output } from '@angular/core';

import { PoiService } from '../shared/poi-list-service/poi-list.service';
import { Router } from '@angular/router';
import { Poi } from '../shared/poi.model';

@Component({
  selector: 'poi-list-component',
  templateUrl: `./poi-list.component.html`,
  styleUrls: [`./poi-list.component.css`]
})

export class PoiListComponent implements OnInit {
  DEFAULT_CITY_ID = 1;

  poiList: Poi[];
  selectedPoi: Poi;
  @Output()
  poiToOutput: EventEmitter<Poi> = new EventEmitter<Poi>();


  constructor(
    private router: Router,
    private poiService: PoiService
  ) { }

  getPoiList(id: number): void {
    this.poiService.getPoiList(id).then(poiList => this.poiList = poiList);
  }

  ngOnInit(): void {
    this.getPoiList(this.DEFAULT_CITY_ID);
  }

  onSelect(poi: Poi): void {
    this.selectPoiInList(poi);
    this.selectPoiMarker(poi);
  }

  selectPoiInList(poi: Poi): void {
    this.selectedPoi = poi;
  }

  selectPoiMarker(poi: Poi): void {
    this.poiToOutput.emit(poi);
  }

  deselectPoiMarker(): void {
    this.poiToOutput.emit(null);
  }

  gotoDetail(poi: Poi): void {
    this.router.navigate(['/poi-detail', poi.cityId, poi.id]);
  }

}
