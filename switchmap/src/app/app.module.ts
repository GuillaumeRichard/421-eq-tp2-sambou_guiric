import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { CitySearchComponent } from './city/city-search/city-search.component';
import { HomeComponent } from './home/home.component';
import { PoiListComponent } from './poi/poi-list/poi-list.component';

@NgModule({
  declarations: [
    AppComponent,
    CitySearchComponent,
    HomeComponent,
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
