import { Component, OnInit } from '@angular/core';

@Component({
  title: string;
  mode: string;
  reachingOut = false;
  submitted = false;
  loadingCategories = false;
  categories: GridData<Category> = new GridData<Category>();

  @ViewChild('search') searchField: ElementRef;

  constructor(
    public dialogRef: MatDialogRef<ProductsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Product,
    private catSrv: CategoryService,
    private productSRv: ProductService
  ) {}

  ngOnInit(): void {
    this.categories.pageSize = 20;
    this.mode = this.data.name === undefined ? 'new' : 'edit';
    this.title = this.mode === 'edit' ? 'ویرایش' : 'جدید';
    this.loadCategoryList();
  }

  loadCategoryList() {
    delete this.categories.data;
    this.loadingCategories = true;
    this.catSrv.get(this.categories).subscribe(res => {
      this.categories = res;
      this.loadingCategories = false;
    });
  }

  searchCategory(input: string) {
    console.log('pox')
    if (!this.categories.filters) {
      this.categories.filters = new Array<Filter>();
      this.categories.filters.push(new Filter("Name", input));
    } else {
      this.categories.filters[0].value = input;
    }

    this.loadCategoryList();
  }

  toggleCatSearch(open: boolean) {
    if (open)
      this.searchField.nativeElement.focus();
  }

  submit() {
    this.submitted = true;
    this.reachingOut = true;

    if (this.mode === 'new') {
      this.productSRv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        swal({title: 'موفق', text: `محصول جدید با موفقیت اضافه گردید`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.productSRv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        swal({title: 'موفق', text: `تغییرات با موفقیت ثبت شد`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
