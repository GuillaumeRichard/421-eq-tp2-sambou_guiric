import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { CitySearchComponent } from './City/city-search/city-search.component';
import { PoiListComponent } from './POI/poi-list/poi-list.component';

@NgModule({
  declarations: [
    AppComponent,
    CitySearchComponent,
    PoiListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
