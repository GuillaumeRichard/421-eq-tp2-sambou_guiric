import { Component } from '@angular/core';
import {  OnInit } from '@angular/core';
import {City} from "../city/shared/city.model";
import {CityService} from "../city/shared/city.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [CityService]
})
export class HomeComponent {

  DEFAULT_CITY_ID = 1;
  selectedCity: City;

  constructor(
    private cityService: CityService
  ) { }

  ngOnInit() : void {
    this.cityService.getCity(this.DEFAULT_CITY_ID)
      .then(city => this.selectedCity = city);
  }

  handleCityUpdated(city: City) : void{
    this.selectedCity = city;
  }

}
