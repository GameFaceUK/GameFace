import { Component } from '@angular/core';

@Component({
  selector: 'game-face-app',
  template: `<h1>Hello {{name}}</h1>`,
})
export class AppComponent {
  name = 'Game Face';
}
