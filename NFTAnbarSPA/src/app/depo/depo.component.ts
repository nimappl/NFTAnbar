import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepoModalComponent } from './depo-modal/depo-modal.component';
import { DepoService } from '../services/depo.service';
import { Depo } from "../models/depo";
import { GridData } from "../models/GridData";
import swal from 'sweetalert';
import { LocationService } from '../services/location.service';
import { City } from '../models/city';
import { Filter } from '../models/filter';

@Component({
  selector: 'app-depo',
  templateUrl: './depo.component.html',
  styleUrls: ['./depo.component.css']
})
export class DepoComponent implements OnInit {

  depos: GridData<Depo> = new GridData<Depo>();

  columns = [
    {name: 'Name', title: 'نام'},
    {name: 'Gcode', title: 'کد انحصاری انبار'},
    {name: 'City', title: 'ناحیه اصلی انبار'},
    {name: 'link', title: 'تنظیمات انبار'}
  ];

  fieldsNotToShow = ['id', 'active', 'DepoType'];
  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  locations = new GridData<City>();
  loadingLocations = false;
  @ViewChild('search') searchField: ElementRef;

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private depoSrv: DepoService,
              private locationSrv: LocationService) { }

  ngOnInit(): void {
    this.depos.filters = new Array<Filter>();
    this.fetch(true);
    this.fetchLocations();
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Depo>();
    pox.filters = this.depos.filters;
    pox.pageNumber = this.depos.pageNumber;
    pox.pageSize = this.depos.pageSize;
    pox.sortBy = this.depos.sortBy;
    pox.sortType = this.depos.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.depoSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.depos = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  fetchLocations() {
    this.loadingLocations = true;
    delete this.locations.data;
    this.locationSrv.get(this.locations).subscribe(res => {
      this.locations = res;
      this.loadingLocations = false;
    });
  }

  toggleLocationSearch(open: boolean) {
    if (open)
      this.searchField.nativeElement.focus();
  }
  searchLocation(input: string) {
    if (!this.locations.filters) {
      this.locations.filters = new Array<Filter>();
      this.locations.filters.push(new Filter("Name", input));
    } else {
      this.locations.filters[0].value = input;
    }

    this.fetchLocations();
  }

  openModal(edit?: Depo) {
    let data: Depo;
    if (edit) data = edit; else data = new Depo();

    const dialogRef = this.dialog.open(DepoModalComponent, {
      width: '750px',
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

  onRemoveLocation(index: number) {
    swal({
      title: 'حذف',
      text: `نوع انبار "${this.depos.data[index].name}" حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.depoSrv.delete(this.depos.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `نوع انبار "${this.depos.data[index].name}" با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.depoSrv.update(this.depos.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `نوع انبار "${this.depos.data[index].name}" با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.depos.data[index].active = !this.depos.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }

}
