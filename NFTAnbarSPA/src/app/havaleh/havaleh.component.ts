import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GridData } from '../models/GridData';
import { Havaleh } from '../models/havaleh';
import { HavalehService } from '../services/havaleh.service';
import { HavalehModalComponent } from './havaleh-modal/havaleh-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-havaleh',
  templateUrl: './havaleh.component.html',
  styleUrls: ['./havaleh.component.css']
})
export class HavalehComponent implements OnInit {
  havalehList: GridData<Havaleh> = new GridData<Havaleh>();

  columns = [{name: 'Name', title: 'عنوان'}];
  fieldsNotToShow = ['id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private havalehSrv: HavalehService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Havaleh>();
    pox.filters = this.havalehList.filters;
    pox.pageNumber = this.havalehList.pageNumber;
    pox.pageSize = this.havalehList.pageSize;
    pox.sortBy = this.havalehList.sortBy;
    pox.sortType = this.havalehList.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.havalehSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.havalehList = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: Havaleh) {
    let data: Havaleh;
    if (edit) data = edit; else data = new Havaleh();

    const dialogRef = this.dialog.open(HavalehModalComponent, {
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
      text: `حواله ${this.havalehList.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.havalehSrv.delete(this.havalehList.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `حواله ${this.havalehList.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.havalehSrv.update(this.havalehList.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `حواله ${this.havalehList.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.havalehList.data[index].active = !this.havalehList.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
