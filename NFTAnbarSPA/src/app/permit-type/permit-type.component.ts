import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GridData } from '../models/GridData';
import { PermitType } from '../models/permitType';
import { PermitTypeService } from '../services/permit-type.service';
import { PermitTypeModalComponent } from './permit-type-modal/permit-type-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-permit-type',
  templateUrl: './permit-type.component.html',
  styleUrls: ['./permit-type.component.css']
})
export class PermitTypeComponent implements OnInit {
  permitTypes: GridData<PermitType> = new GridData<PermitType>();

  columns = [{name: 'Name', title: 'عنوان'}];
  fieldsNotToShow = ['id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private permitTypeSrv: PermitTypeService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<PermitType>();
    pox.filters = this.permitTypes.filters;
    pox.pageNumber = this.permitTypes.pageNumber;
    pox.pageSize = this.permitTypes.pageSize;
    pox.sortBy = this.permitTypes.sortBy;
    pox.sortType = this.permitTypes.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.permitTypeSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.permitTypes = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: PermitType) {
    let data: PermitType;
    if (edit) data = edit; else data = new PermitType();

    const dialogRef = this.dialog.open(PermitTypeModalComponent, {
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
      text: `نوع ارسال ${this.permitTypes.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.permitTypeSrv.delete(this.permitTypes.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `نوع پروانه ${this.permitTypes.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.permitTypeSrv.update(this.permitTypes.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `نوع پروانه ${this.permitTypes.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.permitTypes.data[index].active = !this.permitTypes.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
