import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import {Poi} from "../POI/shared/poi.model";

@Component({
  selector: 'home-component',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  selectedPoi: Poi;

  constructor() { }

  handlePoiUpdated(poi: Poi) {
    this.selectedPoi = poi;
  }

}
