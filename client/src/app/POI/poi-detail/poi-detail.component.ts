import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/switchMap';
import { Poi } from '../shared/poi.model';

import { PoiListService } from '../shared/poi-list-service/poi-list.service';

@Component({
  selector: 'my-poi-detail',
  templateUrl: './poi-detail.component.html',
  styleUrls: [ './poi-detail.component.css' ]
})
export class PoiDetailComponent implements OnInit {
  @Input()
  poi: Poi;
  constructor(
    private poiService: PoiListService,
    private route: ActivatedRoute,
  ) {}
  ngOnInit(): void {
    this.route.params
      .switchMap((params: Params) => this.poiService.getPoi(+params['Id']))
      .subscribe(poi => this.poi = poi);
  }
}
