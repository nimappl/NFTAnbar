import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from 'src/app/models/customer';
import { Naftkesh } from 'src/app/models/naftkesh';
import { Permit } from 'src/app/models/permit';
import { BarnameService } from 'src/app/services/barname.service';
import { ContractorService } from 'src/app/services/contractor.service';
import { CustomerService } from 'src/app/services/customer.service';
import { DepoWorkShiftService } from 'src/app/services/depo-work-shift.service';
import { HavalehService } from 'src/app/services/havaleh.service';
import { LocationService } from 'src/app/services/location.service';
import { NaftkeshService } from 'src/app/services/naftkesh.service';
import { PermitTypeService } from 'src/app/services/permit-type.service';
import { PermitService } from 'src/app/services/permit.service';
import { ProductService } from 'src/app/services/product.service';
import { SendTypeService } from 'src/app/services/send-type.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-permit-modal',
  templateUrl: './permit-modal.component.html',
  styleUrls: ['./permit-modal.component.css']
})
export class PermitModalComponent implements OnInit {
  mode: string;
  title: string;
  reachingOut = false;
  submitted = false;

  naftkesh: Naftkesh = new Naftkesh();
  customer: Customer = new Customer();

  constructor(public dialogRef: MatDialogRef<PermitModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              public permitSrv: PermitService,
              public permitTypeSrv: PermitTypeService,
              public barnameSrv: BarnameService,
              public contractorSrv: ContractorService,
              public customerSrv: CustomerService,
              public depoWorkShiftSrv: DepoWorkShiftService,
              public havalehSrv: HavalehService,
              public locationSrv: LocationService,
              public naftkeshSrv: NaftkeshService,
              public productSrv: ProductService,
              public sendTypeSrv: SendTypeService) { }

  ngOnInit(): void {
    if (!this.data.permitCode) {
      this.mode = 'new';
      this.title = 'جدید'
    } else {
      this.title = 'ویرایش'
      this.mode = 'edit';
    }

    if (this.mode === 'edit') {
      this.naftkeshSrv.getById(this.data.transportNaftkeshId).subscribe(res => {
        this.naftkesh = res;
      });
      this.customerSrv.getById(this.data.customerId).subscribe(res => {
        this.customer = res;
      });
    }
  }

  permitTypeChanged(id: number) {
    this.data.permitTypeId = id;
  }
  barnameChanged(id: number) {
    this.data.barnameId = id;
  }
  naftkeshChanged(id: number) {
    this.data.transportNaftkeshId = id;
    this.naftkeshSrv.getById(id).subscribe(res => {
      this.naftkesh = res;
    });
  }
  productChanged(id: number) {
    this.data.productId = id;
  }
  sendTypeChanged(id: number) {
    this.data.sendTypeId = id;
  }
  locationChanged(id: number) {
    this.data.orgLocationId = id;
  }
  customerChanged(id: number) {
    this.data.customerId = id;
    this.customerSrv.getById(id).subscribe(res => {
      this.customer = res;
    });
  }
  havalehIdChanged(id: number) {
    this.data.havalehId = id;
  }
  contractorChanged(id: number) {
    this.data.contractorId = id;
  }
  workShiftChanged(id: number) {
    this.data.ndepoWorkShiftId = id;
  }

  save() {
    this.reachingOut = true;
    if (this.mode === 'new') {
      this.permitSrv.create(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `پروانه به شناسه ${this.data.permitCode} با موفقیت ثبت شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    } else {
      this.permitSrv.update(this.data).subscribe(res => {
        this.reachingOut = false;
        this.submitted = true;
        swal({title: 'موفق', text: `پروانه به شناسه ${this.data.permitCode} با موفقیت بروز رسانی شد.`, icon: 'success'});
      }, err => {
        this.reachingOut = false;
        swal({title: 'ناموفق', icon: 'error'});
      });
    }
  }
}
