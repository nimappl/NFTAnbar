import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from "@angular/material/input";
import { MatIconModule } from "@angular/material/icon";

import { AppComponent } from './app.component';
import { LayoutComponent } from "./layout/layout.component";
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { HeaderComponent } from './layout/header/header.component';
import { LoadingSpinnerBarsComponent } from './shared/loading-spinner-bars/loading-spinner-bars.component';
import { DataTableComponent } from './shared/data-table/data-table.component';
import { PaginationComponent } from './shared/data-table/pagination/pagination.component';
import { DepoComponent } from './depo/depo.component';
import { DepoTypeComponent } from './depo-type/depo-type.component';
import { LocationComponent } from './location/location.component';
import { LocationModalComponent } from './location/location-modal/location-modal.component';
import { DepoModalComponent } from './depo/depo-modal/depo-modal.component';
import { DepoTypeModalComponent } from './depo-type/depo-type-modal/depo-type-modal.component';
import { SelectWithSearchComponent } from './shared/select-with-search/select-with-search.component';
import { BarnameComponent } from './barname/barname.component';
import { BarnameModalComponent } from './barname/barname-modal/barname-modal.component';
import { ContractorComponent } from './contractor/contractor.component';
import { ContractorModalComponent } from './contractor/contractor-modal/contractor-modal.component';
import { CustomerComponent } from './customer/customer.component';
import { CustomerModalComponent } from './customer/customer-modal/customer-modal.component';
import { HavalehComponent } from './havaleh/havaleh.component';
import { HavalehModalComponent } from './havaleh/havaleh-modal/havaleh-modal.component';
import { NaftkeshComponent } from './naftkesh/naftkesh.component';
import { NaftkeshModalComponent } from './naftkesh/naftkesh-modal/naftkesh-modal.component';
import { DepoWorkShiftComponent } from './depo-work-shift/depo-work-shift.component';
import { DepoWorkShiftModalComponent } from './depo-work-shift/depo-work-shift-modal/depo-work-shift-modal.component';
import { PermitComponent } from './permit/permit.component';
import { PermitModalComponent } from './permit/permit-modal/permit-modal.component';
import { PermitTypeComponent } from './permit-type/permit-type.component';
import { PermitTypeModalComponent } from './permit-type/permit-type-modal/permit-type-modal.component';
import { ProductComponent } from './product/product.component';
import { ProductModalComponent } from './product/product-modal/product-modal.component';
import { SendTypeComponent } from './send-type/send-type.component';
import { SendTypeModalComponent } from './send-type/send-type-modal/send-type-modal.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    SidebarComponent,
    HeaderComponent,
    LoadingSpinnerBarsComponent,
    DataTableComponent,
    PaginationComponent,
    DepoComponent,
    DepoTypeComponent,
    LocationComponent,
    LocationModalComponent,
    DepoModalComponent,
    DepoTypeModalComponent,
    SelectWithSearchComponent,
    BarnameComponent,
    BarnameModalComponent,
    ContractorComponent,
    ContractorModalComponent,
    CustomerComponent,
    CustomerModalComponent,
    HavalehComponent,
    HavalehModalComponent,
    NaftkeshComponent,
    NaftkeshModalComponent,
    DepoWorkShiftComponent,
    DepoWorkShiftModalComponent,
    PermitComponent,
    PermitModalComponent,
    PermitTypeComponent,
    PermitTypeModalComponent,
    ProductComponent,
    ProductModalComponent,
    SendTypeComponent,
    SendTypeModalComponent,
    HomeComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSelectModule,
    MatProgressBarModule,
    MatDialogModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
