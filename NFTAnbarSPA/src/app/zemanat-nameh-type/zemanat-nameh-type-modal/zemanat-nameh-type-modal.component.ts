import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ZemanatNamehTypeService } from 'src/app/services/zemanat-nameh-type.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-zemanat-nameh-type-modal',
  templateUrl: './zemanat-nameh-type-modal.component.html',
  styleUrls: ['./zemanat-nameh-type-modal.component.css']
})
export class ZemanatNamehTypeModalComponent implements OnInit {
  mode: string;
  title: string;
  reachingOut = false;
  submitted = false;

  constructor(public dialogRef: MatDialogRef<ZemanatNamehTypeModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              public zemanatNamehTypeSrv: ZemanatNamehTypeService) { }

  ngOnInit(): void {
    if (!this.data.title) this.data.title = '';
    this.mode = this.data.title.length > 0 ? 'edit' : 'new';
    this.title = this.mode === 'edit' ? 'ویرایش' : 'جدید';
  }

  submit() {
    this.reachingOut = true;
    if (this.mode === 'new') {
      this.zemanatNamehTypeSrv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نوع ضمانتنامه ${this.data.title} با موفقیت ثبت شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.zemanatNamehTypeSrv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نوع ضمانتنامه ${this.data.title} با موفقیت بروز رسانی شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
