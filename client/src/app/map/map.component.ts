import { Component } from '@angular/core';
@Component({
  selector: 'map-component',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent {
  positions = [];
  selectedPos: Position;

  onClick(event) {
    if (event instanceof MouseEvent)
    {
      return;
    }
    this.positions.push(event.latLng);
    event.target.panTo(event.latLng);
  }

  onSelect(pos) {
    this.selectedPos = pos;
  }

}
