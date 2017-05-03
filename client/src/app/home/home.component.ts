import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import {Poi} from "../POI/shared/poi.model";

@Component({
  selector: 'home-component',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  selectedPoi: Poi;

  constructor() { }

  ngOnInit() {
  }

  handlePoiUpdated(poi: Poi) {
    this.selectedPoi = poi;
  }

}
