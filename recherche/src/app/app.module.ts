import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { BrowserModule  } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NguiMapModule} from '@ngui/map';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    NguiMapModule.forRoot({apiUrl: 'https://maps.google.com/maps/api/js?key=AIzaSyAOgYOBdHV3dc4M0bevQIpJ7MvgSyJ9GIU'})
  ],
  declarations: [AppComponent],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
