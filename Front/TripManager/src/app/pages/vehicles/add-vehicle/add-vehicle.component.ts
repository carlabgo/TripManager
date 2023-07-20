import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VehiclesService } from '../vehicles.service';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { Router } from '@angular/router';
import { VehicleResponse, VehicleType } from '../../trips/list/models/vehicle.model';

@Component({
  selector: 'app-add-vehicle',
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css']
})
export class AddVehicleComponent implements OnInit {
  form!:FormGroup;
  vehicleTypes: VehicleType[]=[]
  errorBrand:boolean = false;
  errorType:boolean = false;
  errorLicense:boolean = false;
  isLoading = false;
  @Input() data:VehicleResponse;
  constructor(
    router:Router,
    private fb: FormBuilder,
    private service: VehiclesService,
    private drawerRef: NzDrawerRef<string>,
  ) { }

  ngOnInit() {
    this.getVehicleTypes();
  this.createForm();
    if(this.data){
      this.form.setValue({
        id: this.data.id,
        brand: this.data.brand,
        licensePlate:this.data.licensePlate,
        typeId:this.data.type
      });
    }}
    createForm(){
      this.form = this.fb.group({
        id:[0],
        brand:['', [Validators.required]],
        licensePlate:['', [Validators.required]],
        typeId:[0, [Validators.required]],
      });
    }
    saveVehicle(){
      this.isValidForm();
      if(this.form.valid){
        this.service.createOrUpdateVehicle(this.form.getRawValue())
        .subscribe((r:any) => {
          this.close()
        })
      }
      
    }
    isValidForm(){
      if (this.form.controls['brand'].invalid){
        this.errorBrand = true;
      }
      if (this.form.controls['typeId'].invalid){
        this.errorType = true;
      }
      if (this.form.controls['licensePlate'].invalid){
        this.errorLicense = true;
      }
    }
    close(){
      this.drawerRef.close();
    }
    getVehicleTypes(){
      this.service.getTypes().subscribe((r) => {
        this.vehicleTypes = r.data;
      });
    }
  }