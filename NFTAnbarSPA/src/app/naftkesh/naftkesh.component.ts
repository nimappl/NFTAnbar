import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GridData } from '../models/GridData';
import { Naftkesh } from '../models/naftkesh';
import { NaftkeshService } from '../services/naftkesh.service';
import { NaftkeshModalComponent } from './naftkesh-modal/naftkesh-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-naftkesh',
  templateUrl: './naftkesh.component.html',
  styleUrls: ['./naftkesh.component.css']
})
export class NaftkeshComponent implements OnInit {
  naftkeshList: GridData<Naftkesh> = new GridData<Naftkesh>();

  columns = [
    {name: 'Name', title: 'نام'},
    {name: 'PlateNumber', title: 'پلاک'},
    {name: 'DriverName', title: 'نام راننده'},
    {name: 'ContractorName', title: 'نام پیمانکار'}
  ];
  fieldsNotToShow = ['id', 'active', 'driverNationalCode', 'driverLicenseNumber', 'contractorId'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private naftkeshSrv: NaftkeshService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Naftkesh>();
    pox.filters = this.naftkeshList.filters;
    pox.pageNumber = this.naftkeshList.pageNumber;
    pox.pageSize = this.naftkeshList.pageSize;
    pox.sortBy = this.naftkeshList.sortBy;
    pox.sortType = this.naftkeshList.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.naftkeshSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.naftkeshList = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: Naftkesh) {
    let data: Naftkesh;
    if (edit) data = edit; else data = new Naftkesh();

    const dialogRef = this.dialog.open(NaftkeshModalComponent, {
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
      text: `نفت کش ${this.naftkeshList.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.naftkeshSrv.delete(this.naftkeshList.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `نفت کش ${this.naftkeshList.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.naftkeshSrv.update(this.naftkeshList.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `نفت کش ${this.naftkeshList.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.naftkeshList.data[index].active = !this.naftkeshList.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
