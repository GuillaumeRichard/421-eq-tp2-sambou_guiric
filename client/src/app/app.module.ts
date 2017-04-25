import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { CitySearchComponent } from './City/city-search/city-search.component';
import { MapComponent } from './map/map.component';
import { NguiMapModule} from '@ngui/map';
import { PoiListComponent } from './POI/poi-list/poi-list.component';
import { PoiDetailComponent } from './POI/poi-detail/poi-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    CitySearchComponent,
    MapComponent,
    PoiListComponent,
    PoiDetailComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    NguiMapModule.forRoot({apiUrl: 'https://maps.google.com/maps/api/js?key=AIzaSyAOgYOBdHV3dc4M0bevQIpJ7MvgSyJ9GIU'})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
