import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';

import { DepoComponent } from './depo/depo.component';
import { DepoTypeComponent } from './depo-type/depo-type.component';
import { LocationComponent } from './location/location.component';
import { BarnameComponent } from "./barname/barname.component";
import { ContractorComponent } from "./contractor/contractor.component";
import { CustomerComponent } from "./customer/customer.component";
import { HavalehComponent } from "./havaleh/havaleh.component";
import { NaftkeshComponent } from "./naftkesh/naftkesh.component";
import { DepoWorkShiftComponent } from "./depo-work-shift/depo-work-shift.component";
import { PermitTypeComponent } from "./permit-type/permit-type.component";
import { PermitComponent } from "./permit/permit.component";
import { ProductComponent } from "./product/product.component";
import { SendTypeComponent } from "./send-type/send-type.component";
import { HomeComponent } from "./home/home.component";

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'depo', component: DepoComponent },
  { path: 'depo-type', component: DepoTypeComponent },
  { path: 'location', component: LocationComponent },
  { path: 'barname', component: BarnameComponent },
  { path: 'contractor', component: ContractorComponent },
  { path: 'customer', component: CustomerComponent },
  { path: 'havaleh', component: HavalehComponent },
  { path: 'naftkesh', component: NaftkeshComponent },
  { path: 'depo-work-shift', component: DepoWorkShiftComponent },
  { path: 'permit-type', component: PermitTypeComponent },
  { path: 'permit', component: PermitComponent },
  { path: 'product', component: ProductComponent },
  { path: 'send-type', component: SendTypeComponent },
  { path: '**', redirectTo: '/depo'},
  { path: '', redirectTo: '/depo', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
