import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GridData } from '../models/GridData';
import { Permit } from '../models/permit';
import { PermitService } from '../services/permit.service';
import { PermitModalComponent } from './permit-modal/permit-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-permit',
  templateUrl: './permit.component.html',
  styleUrls: ['./permit.component.css']
})
export class PermitComponent implements OnInit {
  permits: GridData<Permit> = new GridData<Permit>();

  columns = [
    {name: 'BarnameName', title: 'بارنامه'},
    {name: 'CustomerName', title: 'نام مشتری'},
    {name: 'PermitCode', title: 'شناسه پروانه'},
    {name: 'NaftkeshName', title: 'نفتکش'},
    {name: 'ProductName', title: 'فرآورده'}
  ];
  fieldsNotToShow = ['id', 'active', 'havalehName', 'inTheArea', 'isWeightedProduct', 'orgLocationName', 'importExportExchangable', 'owid', 'quantity', 'sendTypeName', 'contractorName', 'wayBill', 'ndepoWorkShiftName', 'permitTypeName'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;

  constructor(private dialog: MatDialog,
              private permitSrv: PermitService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Permit>();
    pox.filters = this.permits.filters;
    pox.pageNumber = this.permits.pageNumber;
    pox.pageSize = this.permits.pageSize;
    pox.sortBy = this.permits.sortBy;
    pox.sortType = this.permits.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.permitSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.permits = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: Permit) {
    let data: Permit;
    if (edit) data = edit; else data = new Permit();

    const dialogRef = this.dialog.open(PermitModalComponent, {
      width: '1100px',
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
      text: `پروانه با شناسه ${this.permits.data[index].permitCode} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.permitSrv.delete(this.permits.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `پروانه با شناسه ${this.permits.data[index].permitCode} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.permitSrv.update(this.permits.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `پروانه با شناسه ${this.permits.data[index].permitCode} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.permits.data[index].active = !this.permits.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
