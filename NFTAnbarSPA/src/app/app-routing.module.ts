import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';

import { DepoComponent } from './depo/depo.component';
import { DepoTypeComponent } from './depo-type/depo-type.component';
import { LocationComponent } from './location/location.component';

const routes: Routes = [
  { path: 'depo', component: DepoComponent },
  { path: 'depo-type', component: DepoTypeComponent },
  { path: 'location', component: LocationComponent },
  { path: '**', redirectTo: '/depo'},
  { path: '', redirectTo: '/depo', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
