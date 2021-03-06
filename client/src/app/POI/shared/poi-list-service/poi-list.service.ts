import { Injectable } from '@angular/core';

import { Poi } from '../poi.model';
import { Http, Headers } from '@angular/http';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class PoiService {
  private poiListUrl = 'http://localhost:59195/api/Cities/';
  private headers = new Headers({'Content-Type': 'application/json'});

  constructor(private http: Http) { }

  getPoiList(id: number): Promise<Poi[]> {
    const url = this.poiListUrl + id + '/pointsofinterest';
    return this.http.get(url, this.headers)
      .toPromise()
      .then(response => response.json().poiList as Poi[])
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }

  getPoi(cityId: number, poiId: number): Promise<Poi> {
    const url = `${this.poiListUrl}${cityId}/pointsofinterest/${poiId}`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json() as Poi)
      .catch(this.handleError);
  }
}
