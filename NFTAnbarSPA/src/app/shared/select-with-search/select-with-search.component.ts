import { Component, ElementRef, Input, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { Filter } from 'src/app/models/filter';
import { GridData } from 'src/app/models/GridData';

@Component({
  selector: 'app-select-with-search',
  templateUrl: './select-with-search.component.html',
  styleUrls: ['./select-with-search.component.css']
})
export class SelectWithSearchComponent implements OnInit {

  @Input() selectedId: any;
  @Input() service;
  @Input() label: string;
  @Output() selectedChanged = new EventEmitter();
  
  list = new GridData<any>();
  loadingList = false;
  @ViewChild('search') searchField: ElementRef;

  ngOnInit(): void {
    this.fetchList();
  }

  fetchList() {
    this.list.pageSize = 20;
    this.loadingList = true;
    delete this.list.data;
    this.service.get(this.list).subscribe(res => {
      this.list = res;
      this.loadingList = false;
    });
  }

  toggleSearch(open: boolean) {
    if (open)
      this.searchField.nativeElement.focus();
  }

  searchList(input: string) {
    if (!this.list.filters) {
      this.list.filters = new Array<Filter>();
      this.list.filters.push(new Filter("Name", input));
    } else {
      this.list.filters[0].value = input;
    }

    this.fetchList();
  }

  selected() {
    this.selectedChanged.emit(this.list.data.find(d => d.id === this.selectedId));
  }

}
