import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Contractor } from '../models/contractor';
import { GridData } from '../models/GridData';
import { ContractorService } from '../services/contractor.service';
import { ContractorModalComponent } from './contractor-modal/contractor-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-contractor',
  templateUrl: './contractor.component.html',
  styleUrls: ['./contractor.component.css']
})
export class ContractorComponent implements OnInit {

  contractors: GridData<Contractor> = new GridData<Contractor>();

  columns = [{name: 'Name', title: 'نام'}];
  fieldsNotToShow = ['id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private contractorSrv: ContractorService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Contractor>();
    pox.filters = this.contractors.filters;
    pox.pageNumber = this.contractors.pageNumber;
    pox.pageSize = this.contractors.pageSize;
    pox.sortBy = this.contractors.sortBy;
    pox.sortType = this.contractors.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.contractorSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.contractors = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: Contractor) {
    let data: Contractor;
    if (edit) data = edit; else data = new Contractor();

    const dialogRef = this.dialog.open(ContractorModalComponent, {
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
      text: `پیمانکار ${this.contractors.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.contractorSrv.delete(this.contractors.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `پیمانکار ${this.contractors.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.contractorSrv.update(this.contractors.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `پیمانکار ${this.contractors.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.contractors.data[index].active = !this.contractors.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }

}
