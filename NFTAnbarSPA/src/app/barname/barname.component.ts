import { Component, OnInit } from '@angular/core';
import { Barname } from '../models/barname';
import { GridData } from '../models/GridData';
import { BarnameModalComponent } from './barname-modal/barname-modal.component';
import { BarnameService } from '../services/barname.service';
import swal from 'sweetalert';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-barname',
  templateUrl: './barname.component.html',
  styleUrls: ['./barname.component.css']
})
export class BarnameComponent implements OnInit {

  barnameList: GridData<Barname> = new GridData<Barname>();

  columns = [{name: 'Name', title: 'عنوان'}];
  fieldsNotToShow = ['id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private barnameSrv: BarnameService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Barname>();
    pox.filters = this.barnameList.filters;
    pox.pageNumber = this.barnameList.pageNumber;
    pox.pageSize = this.barnameList.pageSize;
    pox.sortBy = this.barnameList.sortBy;
    pox.sortType = this.barnameList.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.barnameSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.barnameList = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: Barname) {
    let data: Barname;
    if (edit) data = edit; else data = new Barname();

    const dialogRef = this.dialog.open(BarnameModalComponent, {
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
      text: `بارنامه ${this.barnameList.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.barnameSrv.delete(this.barnameList.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `بارنامه ${this.barnameList.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.barnameSrv.update(this.barnameList.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `بارنامه ${this.barnameList.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.barnameList.data[index].active = !this.barnameList.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }

}
