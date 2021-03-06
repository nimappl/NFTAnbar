import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SendTypeService } from 'src/app/services/send-type.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-send-type-modal',
  templateUrl: './send-type-modal.component.html',
  styleUrls: ['./send-type-modal.component.css']
})
export class SendTypeModalComponent implements OnInit {
  mode: string;
  title: string;
  reachingOut = false;
  submitted = false;

  constructor(public dialogRef: MatDialogRef<SendTypeModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              public sendTypeSrv: SendTypeService) { }

  ngOnInit(): void {
    if (!this.data.name) this.data.name = '';
    this.mode = this.data.name.length > 0 ? 'edit' : 'new';
    this.title = this.mode === 'edit' ? 'ویرایش' : 'جدید';
  }

  submit() {
    this.reachingOut = true;
    if (this.mode === 'new') {
      this.sendTypeSrv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نوع ارسال ${this.data.name} با موفقیت ثبت شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.sendTypeSrv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نوع ارسال ${this.data.name} با موفقیت بروز رسانی شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
