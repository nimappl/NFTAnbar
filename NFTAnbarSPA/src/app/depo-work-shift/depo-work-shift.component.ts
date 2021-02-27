import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepoWorkShift } from '../models/depoWorkShift';
import { GridData } from '../models/GridData';
import { DepoWorkShiftService } from '../services/depo-work-shift.service';
import { DepoWorkShiftModalComponent } from './depo-work-shift-modal/depo-work-shift-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-depo-work-shift',
  templateUrl: './depo-work-shift.component.html',
  styleUrls: ['./depo-work-shift.component.css']
})
export class DepoWorkShiftComponent implements OnInit {
  depoWorkShiftList: GridData<DepoWorkShift> = new GridData<DepoWorkShift>();

  columns = [{name: 'Name', title: 'عنوان'}];
  fieldsNotToShow = ['id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private depoWorkShiftSrv: DepoWorkShiftService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<DepoWorkShift>();
    pox.filters = this.depoWorkShiftList.filters;
    pox.pageNumber = this.depoWorkShiftList.pageNumber;
    pox.pageSize = this.depoWorkShiftList.pageSize;
    pox.sortBy = this.depoWorkShiftList.sortBy;
    pox.sortType = this.depoWorkShiftList.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.depoWorkShiftSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.depoWorkShiftList = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: DepoWorkShift) {
    let data: DepoWorkShift;
    if (edit) data = edit; else data = new DepoWorkShift();

    const dialogRef = this.dialog.open(DepoWorkShiftModalComponent, {
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
      text: `شیفت ${this.depoWorkShiftList.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.depoWorkShiftSrv.delete(this.depoWorkShiftList.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `شیفت ${this.depoWorkShiftList.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.depoWorkShiftSrv.update(this.depoWorkShiftList.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `شیفت ${this.depoWorkShiftList.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.depoWorkShiftList.data[index].active = !this.depoWorkShiftList.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
