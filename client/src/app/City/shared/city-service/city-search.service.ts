import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { City } from '../city.model';





@Injectable()
export class CitySearchService {
  constructor(private http: Http) {}
  search(term: string): Observable<City[]> {
    return this.http
      .get(`localhost:59195/api/Cities/${term}`)  // ******ATTENTION, À REFAIRE SELON LE SERVEUR
      .map(response => response.json().data as City[]);
  }
}
