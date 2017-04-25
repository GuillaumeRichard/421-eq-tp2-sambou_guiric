import { Component, OnInit } from '@angular/core';
import { Poi } from '../shared/poi.model';
import { PoiService } from '../shared/poi-list-service/poi-list.service';
import { Router } from '@angular/router';


@Component({
  selector: 'my-poiList',
  templateUrl: `./poi-list.component.html`,
  styleUrls: [`./poi-list.component.css`]
})

export class PoiListComponent implements OnInit {
  poiList: Poi[];
  selectedPoi: Poi;

  constructor(
    private router: Router,
    private poiService: PoiService) { }

  getPoiList(): void {
    this.poiService.getPoiList().then(poiList => this.poiList = poiList);
  }
  ngOnInit(): void {
    this.getPoiList();
  }

  onSelect(poi: Poi): void {
    this.selectedPoi = poi;
  }

  gotoDetail(): void {
    this.router.navigate(['/detail', this.selectedPoi.Id]);
  }

}
