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
import { DataTableComponent } from './data-table/data-table.component';
import { PaginationComponent } from './data-table/pagination/pagination.component';
import { DepoComponent } from './depo/depo.component';
import { DepoTypeComponent } from './depo-type/depo-type.component';
import { LocationComponent } from './location/location.component';
import { LocationModalComponent } from './location/location-modal/location-modal.component';
import { DepoModalComponent } from './depo/depo-modal/depo-modal.component';
import { DepoTypeModalComponent } from './depo-type/depo-type-modal/depo-type-modal.component';

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
    DepoTypeModalComponent
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