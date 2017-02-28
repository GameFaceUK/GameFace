import { ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';

@Component({
    templateUrl: 'app/profile/profile.component.html'
})

export class ProfileComponent{

    user: string;

    constructor(private route: ActivatedRoute){

    }

    ngOnInit(){
        this.user = this.route.snapshot.params['user'];
    }

}