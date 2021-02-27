import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PermitTypeService } from 'src/app/services/permit-type.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-permit-type-modal',
  templateUrl: './permit-type-modal.component.html',
  styleUrls: ['./permit-type-modal.component.css']
})
export class PermitTypeModalComponent implements OnInit {
  mode: string;
  title: string;
  reachingOut = false;
  submitted = false;

  constructor(public dialogRef: MatDialogRef<PermitTypeModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              public permitTypeSrv: PermitTypeService) { }

  ngOnInit(): void {
    if (!this.data.name) this.data.name = '';
    this.mode = this.data.name.length > 0 ? 'edit' : 'new';
    this.title = this.mode === 'edit' ? 'ویرایش' : 'جدید';
  }

  submit() {
    this.reachingOut = true;
    if (this.mode === 'new') {
      this.permitTypeSrv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نوع پروانه ${this.data.name} با موفقیت ثبت شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.permitTypeSrv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نوع پروانه ${this.data.name} با موفقیت بروز رسانی شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
