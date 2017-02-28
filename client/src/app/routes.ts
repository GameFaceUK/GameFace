import { Routes } from '@angular/router';
import { CommonVO } from './common/common.vo';
import { HomeComponent } from './home/home.component';

export const appRoutes: Routes = [
    { path: CommonVO.Pages.Home, component: HomeComponent },
    { path: '', redirectTo: CommonVO.Pages.Home, pathMatch: 'full' },
]