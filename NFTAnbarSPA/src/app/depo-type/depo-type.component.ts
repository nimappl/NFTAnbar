import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepoTypeModalComponent } from './depo-type-modal/depo-type-modal.component';
import { DepoTypeService } from '../services/depo-type.service';
import { DepoType } from "../models/depoType";
import { GridData } from "../models/GridData";
import swal from 'sweetalert';

@Component({
  selector: 'app-depo-type',
  templateUrl: './depo-type.component.html',
  styleUrls: ['./depo-type.component.css']
})
export class DepoTypeComponent implements OnInit {

  depoTypes: GridData<DepoType> = new GridData<DepoType>();

  columns = [
    {name: 'Name', title: 'نام'},
    {name: 'Gcode', title: 'کد انحصاری'},
    {name: 'link', title: 'انبار ها'}
  ];

  fieldsNotToShow = ['id', 'active', 'gkey'];
  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private depoTypeSrv: DepoTypeService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<DepoType>();
    pox.filters = this.depoTypes.filters;
    pox.pageNumber = this.depoTypes.pageNumber;
    pox.pageSize = this.depoTypes.pageSize;
    pox.sortBy = this.depoTypes.sortBy;
    pox.sortType = this.depoTypes.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.depoTypeSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.depoTypes = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: DepoType) {
    let data: DepoType;
    if (edit) data = edit; else data = new DepoType();

    const dialogRef = this.dialog.open(DepoTypeModalComponent, {
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

  openDepo(id: number) {

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
      text: `نوع انبار "${this.depoTypes.data[index].name}" حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.depoTypeSrv.delete(this.depoTypes.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `نوع انبار "${this.depoTypes.data[index].name}" با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.depoTypeSrv.update(this.depoTypes.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `نوع انبار "${this.depoTypes.data[index].name}" با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.depoTypes.data[index].active = !this.depoTypes.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }

}
