import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Contractor } from 'src/app/models/contractor';
import { ContractorService } from 'src/app/services/contractor.service';
import { NaftkeshService } from 'src/app/services/naftkesh.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-naftkesh-modal',
  templateUrl: './naftkesh-modal.component.html',
  styleUrls: ['./naftkesh-modal.component.css']
})
export class NaftkeshModalComponent implements OnInit {
  mode: string;
  title: string;
  reachingOut = false;
  submitted = false;

  constructor(public dialogRef: MatDialogRef<NaftkeshModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              public naftkeshSrv: NaftkeshService,
              public contractorSrv: ContractorService) { }

  ngOnInit(): void {
    if (!this.data.name) this.data.name = '';
    this.mode = this.data.name.length > 0 ? 'edit' : 'new';
    this.title = this.mode === 'edit' ? 'ویرایش' : 'جدید';
  }

  contractorChanged(c: Contractor) {
    this.data.contractorId = c.id;
    // this.data.contractorName = c.name;
  }

  submit() {
    this.reachingOut = true;
    if (this.mode === 'new') {
      this.naftkeshSrv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نفت کش ${this.data.name} با موفقیت ثبت شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.naftkeshSrv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `نفت کش ${this.data.name} با موفقیت بروز رسانی شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
