import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
// Observable class extensions
import 'rxjs/add/observable/of';
// Observable operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/switchMap';

import {City} from '../shared/city.model';
import {CitySearchService} from '../shared/city-service/city-search.service';

@Component({
  selector: 'app-city-search',
  templateUrl: './city-search.component.html',
  styleUrls: ['./city-search.component.css']
})
export class CitySearchComponent implements OnInit {

  cities: Observable<City[]>;
  private searchTerms = new Subject<string>();

  constructor(
    private citySearchService: CitySearchService,
    private router: Router) {}
  // Push a search term into the observable stream.
  search(term: string): void {
    this.searchTerms.next(term);
  }

  ngOnInit() {
    this.cities = this.searchTerms
      .debounceTime(300)        // wait 300ms after each keystroke before considering the term
      .distinctUntilChanged()   // ignore if next search term is same as previous
      .switchMap(term => term   // switch to new observable each time the term changes
        // return the http search observable
        ? this.citySearchService.search(term)
        // or the observable of empty heroes if there was no search term
        : Observable.of<City[]>([]))
      .catch(error => {
        // TODO: add real error handling
        console.log(error);
        return Observable.of<City[]>([]);
      });

  }

  gotoMap(city: City): void {
    const link = ['/home', city.Id];
    this.router.navigate(link);
  }
}
