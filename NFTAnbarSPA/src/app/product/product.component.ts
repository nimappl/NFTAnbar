import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GridData } from '../models/GridData';
import { Product } from '../models/product';
import { ProductService } from '../services/product.service';
import { ProductModalComponent } from './product-modal/product-modal.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: GridData<Product> = new GridData<Product>();

  columns = [{name: 'Name', title: 'نام'}];
  fieldsNotToShow = ['id', 'active'];

  showSearchField = false;
  loading = false;
  sorting = false;
  loadingFailed = false;
  searchFormStatus = 'clean';

  activeDeactive: boolean = true;

  constructor(private dialog: MatDialog,
              private productsSrv: ProductService) { }

  ngOnInit(): void {
    this.fetch(true);
  }

  fetch(tableLoading = false) {
    let pox  = new GridData<Product>();
    pox.filters = this.products.filters;
    pox.pageNumber = this.products.pageNumber;
    pox.pageSize = this.products.pageSize;
    pox.sortBy = this.products.sortBy;
    pox.sortType = this.products.sortType;
    this.loading = true;
    this.sorting = tableLoading;
    this.productsSrv.get(pox).subscribe(res => {
      this.loading = false;
      this.sorting = false;
      this.products = res;
    }, err => {
      this.loading = false;
      this.sorting = false;
      this.loadingFailed = true;
    });
  }

  openModal(edit?: Product) {
    let data: Product;
    if (edit) data = edit; else data = new Product();

    const dialogRef = this.dialog.open(ProductModalComponent, {
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
      text: `فرآورده ${this.products.data[index].name} حذف خواهد شد`,
      icon: 'warning',
      buttons: ['بازگشت', 'ادامه'],
      dangerMode: true
    }).then(deleteConfirm => {
      if (deleteConfirm) {
        this.loading = true;
        this.productsSrv.delete(this.products.data[index].id).subscribe(res => {
          this.loading = false;
          swal({title: 'موفق', text: `فرآورده ${this.products.data[index].name} با موفقیت حذف شد.`, icon: 'success'});
          this.fetch();
        }, err => {
          this.loading = false;
          swal({title: 'ناموفق', icon: 'error'});
        });
      }
    });
  }

  onToggleStatus(index: number) {
    this.productsSrv.update(this.products.data[index]).subscribe(res => {
      swal({title: 'موفق', text: `فرآورده ${this.products.data[index].name} با موفقیت بروز رسانی شد.`, icon: 'success'});
    }, err => {
      this.products.data[index].active = !this.products.data[index].active;
      swal({title: 'ناموفق', icon: 'error'});
    });
  }
}
