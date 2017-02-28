import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  moduleId: module.id,
  selector: 'home-app',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  defaultPlayer: string = 'Pick player...';
  users: string[];
  selectedPlayer: string = this.defaultPlayer;

  constructor(private router: Router) {

  }

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


  public selectPlayer(value: any) {
    if (value !== 0) {
      this.selectedPlayer = value;
    }
  }

  public viewProfile() {
    this.router.navigate(['/profile', this.selectedPlayer]);
  }

  public canViewProfile(): boolean {
    let validPlayer = this.users.find(x => x === this.selectedPlayer);
    return validPlayer !== undefined && validPlayer !== this.defaultPlayer;
  }

}
