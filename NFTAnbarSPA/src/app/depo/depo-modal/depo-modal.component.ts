import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Depo } from 'src/app/models/depo';
import { DepoTypeService } from 'src/app/services/depo-type.service';
import { DepoService } from 'src/app/services/depo.service';
import { LocationService } from 'src/app/services/location.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-depo-modal',
  templateUrl: './depo-modal.component.html',
  styleUrls: ['./depo-modal.component.css']
})
export class DepoModalComponent implements OnInit {
  title: string;
  mode: string;
  reachingOut = false;
  submitted = false;

  constructor(
    public dialogRef: MatDialogRef<DepoModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Depo,
    private depoSrv: DepoService,
    public locationSrv: LocationService,
    public depoTypeSrv: DepoTypeService
  ) {}

  ngOnInit(): void {
    this.mode = this.data.name === undefined ? 'new' : 'edit';
    this.title = this.mode === 'edit' ? 'ویرایش' : 'جدید';
  }

  setLocation(id: number) {
    this.data.cityId = id;
  }

  setDepoType(id: number) {
    this.data.ndepoTypeId = id;
  }

  submit() {
    this.submitted = true;
    this.reachingOut = true;

    if (this.mode === 'new') {
      this.depoSrv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        swal({title: 'موفق', text: `انبار ${this.data.name} با موفقیت ثبت شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.depoSrv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        swal({title: 'موفق', text: `انبار ${this.data.name} با موفقیت بروز رسانی شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
