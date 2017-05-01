import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NguiMapModule } from '@ngui/map';

import { AppComponent } from './app.component';
import { CitySearchComponent } from './City/city-search/city-search.component';
import { MapComponent } from './map/map.component';
import { PoiListComponent } from './POI/poi-list/poi-list.component';
import {CitySearchService} from './City/shared/city-service/city-search.service';
import {PoiService} from './POI/shared/poi-list-service/poi-list.service';
import {AppRoutingModule} from './app-routing.module';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    CitySearchComponent,
    MapComponent,
    PoiListComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    AppRoutingModule,
    NguiMapModule.forRoot({apiUrl: 'https://maps.google.com/maps/api/js?key=AIzaSyAOgYOBdHV3dc4M0bevQIpJ7MvgSyJ9GIU'})
  ],
  providers: [
    CitySearchService,
    PoiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
