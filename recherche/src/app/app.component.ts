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
      //Je comprends pas
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
