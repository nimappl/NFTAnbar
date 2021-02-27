import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LocationModalComponent } from './location-modal/location-modal.component';
import { LocationService } from '../services/location.service';
import { City } from "../models/city";
import { GridData } from "../models/GridData";
import swal from 'sweetalert';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements OnInit {

  cities: GridData<City> = new GridData<City>();

  columns = [{name: 'Name', title: 'نام'}];
  fieldsNotToShow = ['description', 'id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private locationSrv: LocationService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<City>();
    pox.filters = this.cities.filters;
    pox.pageNumber = this.cities.pageNumber;
    pox.pageSize = this.cities.pageSize;
    pox.sortBy = this.cities.sortBy;
    pox.sortType = this.cities.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.locationSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.cities = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: City) {
    let data: City;
    if (edit) data = edit; else data = new City();

    const dialogRef = this.dialog.open(LocationModalComponent, {
      width: '650px',
      direction: 'rtl',
      disableClose: true,
      data: data
    });

    dialogRef.afterClosed().subscribe(submitted => {
      if (submitted)
        this.fetch(true);
    });
  }

  toggleSearch() {
    this.showSearchField = !this.showSearchField;
  }

  paramsChanged() {
    this.fetch();
  }

  onRemoveItem(index: number) {
    swal({
      title: 'حذف',
      text: `ناحیه ${this.cities.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.locationSrv.delete(this.cities.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `ناحیه ${this.cities.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.locationSrv.update(this.cities.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `ناحیه ${this.cities.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.cities.data[index].active = !this.cities.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }

}
