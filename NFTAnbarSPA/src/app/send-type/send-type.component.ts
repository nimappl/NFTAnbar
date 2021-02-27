import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GridData } from '../models/GridData';
import { SendType } from '../models/sendType';
import { SendTypeService } from '../services/send-type.service';
import { SendTypeModalComponent } from './send-type-modal/send-type-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-send-type',
  templateUrl: './send-type.component.html',
  styleUrls: ['./send-type.component.css']
})
export class SendTypeComponent implements OnInit {
  sendTypes: GridData<SendType> = new GridData<SendType>();

  columns = [{name: 'Name', title: 'عنوان'}];
  fieldsNotToShow = ['id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private sendTypeSrv: SendTypeService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<SendType>();
    pox.filters = this.sendTypes.filters;
    pox.pageNumber = this.sendTypes.pageNumber;
    pox.pageSize = this.sendTypes.pageSize;
    pox.sortBy = this.sendTypes.sortBy;
    pox.sortType = this.sendTypes.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.sendTypeSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.sendTypes = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: SendType) {
    let data: SendType;
    if (edit) data = edit; else data = new SendType();

    const dialogRef = this.dialog.open(SendTypeModalComponent, {
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
      text: `نوع ارسال ${this.sendTypes.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.sendTypeSrv.delete(this.sendTypes.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `نوع ارسال ${this.sendTypes.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.sendTypeSrv.update(this.sendTypes.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `نوع ارسال ${this.sendTypes.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.sendTypes.data[index].active = !this.sendTypes.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
