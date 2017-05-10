import {Component, EventEmitter} from '@angular/core';
import {Output} from '@angular/core';
import {OnInit} from '@angular/core';
import {CityService} from "../shared/city.service";
import {Observable, Subject} from "rxjs";
import {City} from "../shared/city.model";

import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'city-search-component',
  templateUrl: './city-search.component.html',
  styleUrls: ['./city-search.component.css'],
  providers: [CityService]
})

export class CitySearchComponent implements OnInit {
  @Output()
  cityUpdate: EventEmitter<City> = new EventEmitter<City>();

  cities: Observable<City[]>;
  private searchTerms = new Subject<string>();

  constructor(
    private citySearchService: CityService
  ) { }

  ngOnInit(): void {
    this.cities = this.searchTerms
      .debounceTime(300)
      .distinctUntilChanged()
      .switchMap(term => term
        ? this.citySearchService.search(term)
        : Observable.of<City[]>([]))
      .catch(error => {
        console.log(error);
        return Observable.of<City[]>([]);
      });
  }

  search(term : string) : void {
    this.searchTerms.next(term);
  }

  updateCity(city: City): void {
    this.cityUpdate.emit(city);
  }
}
