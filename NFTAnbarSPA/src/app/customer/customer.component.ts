import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Customer } from '../models/customer';
import { GridData } from '../models/GridData';
import { CustomerService } from '../services/customer.service';
import { CustomerModalComponent } from './customer-modal/customer-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customers: GridData<Customer> = new GridData<Customer>();

  columns = [
    {name: 'Name', title: 'نام'},
    {name: 'Gcode', title: 'کد انحصاری'},
  ];
  fieldsNotToShow = ['id', 'active', 'nationalCode', 'gkey'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private customerSrv: CustomerService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Customer>();
    pox.filters = this.customers.filters;
    pox.pageNumber = this.customers.pageNumber;
    pox.pageSize = this.customers.pageSize;
    pox.sortBy = this.customers.sortBy;
    pox.sortType = this.customers.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.customerSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.customers = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: Customer) {
    let data: Customer;
    if (edit) data = edit; else data = new Customer();

    const dialogRef = this.dialog.open(CustomerModalComponent, {
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
      text: `مشتری ${this.customers.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.customerSrv.delete(this.customers.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `مشتری ${this.customers.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.customerSrv.update(this.customers.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `مشتری ${this.customers.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.customers.data[index].active = !this.customers.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
