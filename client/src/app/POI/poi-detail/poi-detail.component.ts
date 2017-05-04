import { Component, OnInit } from '@angular/core';
import {Poi} from "../shared/poi.model";
import {PoiService} from "../shared/poi-list-service/poi-list.service";
import { ActivatedRoute, Params }   from '@angular/router';
import { Location }                 from '@angular/common';
import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'app-poi-detail',
  templateUrl: './poi-detail.component.html',
  styleUrls: ['./poi-detail.component.css']
})
export class PoiDetailComponent implements OnInit {

  poiabc: Poi;

  constructor(
    private poiService: PoiService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.poiService.getPoi(+params['city-id'], +params['poi-id']))
      .subscribe(poi => {
        this.poiabc = poi;
        console.log("*****************************************a");
        console.log(poi);
        console.log("*****************************************b");
        console.log(this.poiabc);

      });
  //
  //   this.poiabc = {
  //     id: 1,
  //   name: "a",
  //   description: "a",
  //   address: "a",
  //   longitude: "a",
  //   latitude: "a",
  //   cityId: 1
  // };

    alert(this.poiabc);
}
}
