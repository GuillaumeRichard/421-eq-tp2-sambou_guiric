import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { City } from './city.model';
import 'rxjs/add/operator/map';

@Injectable()
export class CityService {

  constructor(private http: Http) { }

  search(term: string) : Observable<City[]> {
    return this.http
      .get(`http::/localhost:59195/api/Cities/${term}?includePointsOfInterest=true`)
      .map(response => response.json() as City[]);
  }

  getCity(cityId: number): Promise<City> {
    const url = `http://localhost:59195/api/Cities/${cityId}?includePointsOfInterest=true`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json() as City)
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
