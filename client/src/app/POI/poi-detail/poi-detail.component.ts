import { Component, OnInit } from '@angular/core';
import {Poi} from "../shared/poi.model";
import {PoiService} from "../shared/poi-list-service/poi-list.service";
import {ActivatedRoute, Params, Router}   from '@angular/router';
import { Location }                 from '@angular/common';
import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'app-poi-detail',
  templateUrl: './poi-detail.component.html',
  styleUrls: ['./poi-detail.component.css']
})
export class PoiDetailComponent implements OnInit {

  poi: Poi;

  constructor(
    private poiService: PoiService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.poiService.getPoi(+params['city-id'], +params['poi-id']))
      .subscribe(poi => {
        this.poi = poi;
      });

  }

  goBack() {
    this.location.back();
  }
}
