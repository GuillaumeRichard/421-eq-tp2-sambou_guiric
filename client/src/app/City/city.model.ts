import Poi from '../POI/poi.model';

export class City {
  Id: number;
  Name: string;
  Country: string;
  Population: number;
  PoiList: Poi[];
}
