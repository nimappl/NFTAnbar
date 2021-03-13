import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GridData } from '../models/GridData';
import { ZemanatNamehType } from '../models/zemanat-nameh-type';
import { ZemanatNamehTypeService } from '../services/zemanat-nameh-type.service';
import { ZemanatNamehTypeModalComponent } from './zemanat-nameh-type-modal/zemanat-nameh-type-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-zemanat-nameh-type',
  templateUrl: './zemanat-nameh-type.component.html',
  styleUrls: ['./zemanat-nameh-type.component.css']
})
export class ZemanatNamehTypeComponent implements OnInit {
  zemanatNamehTypes: GridData<ZemanatNamehType> = new GridData<ZemanatNamehType>();

  columns = [
    {name: 'Title', title: 'عنوان'},
    {name: 'Gkey', title: 'کد دسترسی برنامه نویس'}
  ];
  fieldsNotToShow = ['id', 'active', 'gdesc'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private zemanatNamehTypeSrv: ZemanatNamehTypeService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<ZemanatNamehType>();
    pox.filters = this.zemanatNamehTypes.filters;
    pox.pageNumber = this.zemanatNamehTypes.pageNumber;
    pox.pageSize = this.zemanatNamehTypes.pageSize;
    pox.sortBy = this.zemanatNamehTypes.sortBy;
    pox.sortType = this.zemanatNamehTypes.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.zemanatNamehTypeSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.zemanatNamehTypes = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: ZemanatNamehType) {
    let data: ZemanatNamehType;
    if (edit) data = edit; else data = new ZemanatNamehType();

    const dialogRef = this.dialog.open(ZemanatNamehTypeModalComponent, {
      width: '800px',
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
      text: `نوع ضمانتنامه ${this.zemanatNamehTypes.data[index].title} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.zemanatNamehTypeSrv.delete(this.zemanatNamehTypes.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `نوع ضمانتنامه ${this.zemanatNamehTypes.data[index].title} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.zemanatNamehTypeSrv.update(this.zemanatNamehTypes.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `نوع ضمانتنامه ${this.zemanatNamehTypes.data[index].title} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.zemanatNamehTypes.data[index].active = !this.zemanatNamehTypes.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
