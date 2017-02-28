import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'home-app',
  templateUrl: 'app/home/home.component.html',
  styleUrls: ['app/home/home.component.css']
})
export class HomeComponent implements OnInit {

  users: string[];

  ngOnInit() {
    this.users = [
      'Alex',
      'Edgar',
      'Elena',
      'James',
      'Marlon',
      'Matt',
    ]
  }



}
