import {Component, OnInit} from '@angular/core';
@Component({
  selector: 'map-component',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
  positions = [];
  selectedPos: Position;

  ngOnInit(): void {

  }

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
