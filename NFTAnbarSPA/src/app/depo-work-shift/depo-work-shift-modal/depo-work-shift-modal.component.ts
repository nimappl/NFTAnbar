import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DepoWorkShiftService } from 'src/app/services/depo-work-shift.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-depo-work-shift-modal',
  templateUrl: './depo-work-shift-modal.component.html',
  styleUrls: ['./depo-work-shift-modal.component.css']
})
export class DepoWorkShiftModalComponent implements OnInit {
  mode: string;
  title: string;
  reachingOut = false;
  submitted = false;

  constructor(public dialogRef: MatDialogRef<DepoWorkShiftModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              public depoWorkShiftSrv: DepoWorkShiftService) { }

  ngOnInit(): void {
    if (!this.data.name) this.data.name = '';
    this.mode = this.data.name.length > 0 ? 'edit' : 'new';
    this.title = this.mode === 'edit' ? 'ویرایش' : 'جدید';
  }

  submit() {
    this.reachingOut = true;
    if (this.mode === 'new') {
      this.depoWorkShiftSrv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `شیفت ${this.data.name} با موفقیت ثبت شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.depoWorkShiftSrv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `شیفت ${this.data.name} با موفقیت بروز رسانی شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
