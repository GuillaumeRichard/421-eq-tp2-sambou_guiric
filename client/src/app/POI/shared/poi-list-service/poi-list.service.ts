import { Injectable } from '@angular/core';

import { Poi } from '../poi.model';
import { Http, Headers } from '@angular/http';
import { POIS } from '../mock-pois';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class PoiService {
  private poiListUrl = 'http://localhost:59195/api/Cities/';
  private headers = new Headers({'Content-Type': 'application/json'});

  constructor(private http: Http) { }

  getPoiList(name: string): Promise<Poi[]> {
    const url = this.poiListUrl + name + '/pointsofinterest';
      let poiList = this.http.get(url, {headers: this.headers});
      let promise = poiList.toPromise().then(response => response.json().data as Poi[]).catch(this.handleError);

      return promise;
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }

  getPoi(id: number): Promise<Poi> {
    const url = `${this.poiListUrl}/${id}`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json().data as Poi)
      .catch(this.handleError);
  }
}
