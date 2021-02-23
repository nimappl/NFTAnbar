import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepoModalComponent } from './depo-modal/depo-modal.component';
import { DepoService } from '../services/depo.service';
import { Depo } from "../models/depo";
import { GridData } from "../models/GridData";
import swal from 'sweetalert';
import { Filter } from '../models/filter';
import { ActivatedRoute } from '@angular/router';
import { DepoTypeService } from '../services/depo-type.service';

@Component({
  selector: 'app-depo',
  templateUrl: './depo.component.html',
  styleUrls: ['./depo.component.css']
})
export class DepoComponent implements OnInit {

  depos: GridData<Depo> = new GridData<Depo>();

  columns = [
    {name: 'Name', title: 'نام'},
    {name: 'Gcode', title: 'کد انحصاری'},
    {name: 'City', title: 'ناحیه اصلی انبار'},
    {name: 'NdepoType', title: 'نوع انبار'},
    {name: 'link', title: 'تنظیمات انبار'}
  ];

  depoTypeId: number;

  fieldsNotToShow = ['id', 'active'];
  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private depoSrv: DepoService,
              public depoTypeSrv: DepoTypeService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    if (!this.depos.filters)
      this.depos.filters = new Array<Filter>();

    this.route.queryParams.subscribe(qParams => {
      if (qParams.depoTypeId) {
        this.depoTypeId = +qParams.depoTypeId;
        this.filterByDepoType(+qParams.depoTypeId);
      } else {
        this.depos.filters = [];
      }
    });

    this.fetch(true);
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

  filterByDepoType(id: number) {
    let filter = this.depos.filters.find(f => f.key === 'NdepoTypeId');
    
    if (!filter) {
      this.depos.filters.push(new Filter('NdepoTypeId', id.toString()));
    } else {
      filter.value = id.toString();
    }

    this.depoTypeId = id;
    this.fetch();
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
      text: `انبار "${this.depos.data[index].name}" حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.depoSrv.delete(this.depos.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `انبار "${this.depos.data[index].name}" با موفقیت حذف شد.`, icon: 'success'});
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
