import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  positions = [];
  selectedPos: Position;

  onClick(event) {
    let position = event.latLng;
    let latitude = position.latitude;
    let longitude = position.longitude;
    let content = "longitude: " + longitude + ", latitude: " + latitude;
    let infoWindow = new google.maps.InfoWindow({
      content: content,
      position: position
    });
    infoWindow.open();
  }

  onSelect(pos) {
    this.selectedPos = pos;
  }

}
