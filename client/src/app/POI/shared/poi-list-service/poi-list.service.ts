import { Injectable } from '@angular/core';

import { Poi } from '../poi.model';
import { Http, Headers } from '@angular/http';
import { POIS } from '../mock-pois';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class PoiListService {
  private poiListUrl = 'api/PoiList';  // ******À CHANGER
  private headers = new Headers({'Content-Type': 'application/json'});

  constructor(private http: Http) { }


  getPoiList(): Promise<Poi[]> {
    return Promise.resolve(POIS);
    // return this.http.get(this.poiListUrl)
    //   .toPromise()
    //   .then(response  => response.json().data as Poi[])
    //   .catch(this.handleError);
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
