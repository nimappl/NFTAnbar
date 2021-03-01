import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from 'src/app/models/customer';
import { Naftkesh } from 'src/app/models/naftkesh';
import { Permit } from 'src/app/models/permit';
import { PermitType } from 'src/app/models/permitType';
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
  permitType: string;
  sendType: string;

  constructor(public dialogRef: MatDialogRef<PermitModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Permit,
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
      this.title = 'جدید';
    } else {
      this.title = 'ویرایش';
      this.mode = 'edit';
      this.naftkeshSrv.getById(this.data.transportNaftkeshId).subscribe(res => {
        this.naftkesh = res;
      });
      this.customerSrv.getById(this.data.customerId).subscribe(res => {
        this.customer = res;
      });
    }
    this.shitChanged();
  }

  shitChanged() {
    if (this.data.permitTypeId) {
      if (this.data.permitTypeId == 2)
          this.permitType = 'bargiri';
      if (this.data.permitTypeId == 3 || this.data.permitTypeId == 4 || this.data.permitTypeId == 5)
        this.permitType = 'enheraf';
      if (this.data.permitTypeId == 6)
        this.permitType = 'shostoshu';
    }

    if (this.data.sendTypeId) {
      if (this.data.sendTypeId == 2)
        this.sendType = 'localcustomer';
      if (this.data.sendTypeId == 3)
        this.sendType = 'tadarokati';
    }
  }

  permitTypeChanged(value: PermitType) {
    this.data.permitTypeId = value.id;
    this.data.permitTypeName = value.name;
    this.shitChanged();
  }
  barnameChanged(id: number) {
    this.data.barnameId = id;
    this.shitChanged();
  }
  naftkeshChanged(id: number) {
    this.data.transportNaftkeshId = id;
    this.naftkeshSrv.getById(id).subscribe(res => {
      this.naftkesh = res;
    });
    this.shitChanged();
  }
  productChanged(id: number) {
    this.data.productId = id;
    this.shitChanged();
  }
  sendTypeChanged(id: number) {
    this.data.sendTypeId = id;
    this.shitChanged();
  }
  locationChanged(id: number) {
    this.data.orgLocationId = id;
    this.shitChanged();
  }
  customerChanged(id: number) {
    this.data.customerId = id;
    this.customerSrv.getById(id).subscribe(res => {
      this.customer = res;
    });
    this.shitChanged();
  }
  havalehIdChanged(id: number) {
    this.data.havalehId = id;
    this.shitChanged();
  }
  contractorChanged(id: number) {
    this.data.contractorId = id;
    this.shitChanged();
  }
  workShiftChanged(id: number) {
    this.data.ndepoWorkShiftId = id;
    this.shitChanged();
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
