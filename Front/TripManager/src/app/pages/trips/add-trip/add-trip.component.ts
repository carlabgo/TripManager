import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TripsService } from '../trips.service';
import { NzDrawerRef} from 'ng-zorro-antd/drawer'
import { CityDTO } from '../list/models/city.model';
import { VehicleDTO } from '../list/models/vehicle.model';

@Component({
  selector: 'app-add-trip',
  templateUrl: './add-trip.component.html',
  styleUrls: ['./add-trip.component.css']
})
export class AddTripComponent implements OnInit {
  form!:FormGroup;
  isLoading = false;
  cities: CityDTO[]=[]
  vehicles: VehicleDTO[]=[]
  errorName:boolean = false;
  errorDescription:boolean = false;
  errorCity:boolean = false;
  errorVehicle:boolean =false;
  errorDate:boolean = false;

  @Input() idTrip:number;

  constructor(
    router:Router,
    private fb: FormBuilder,
    private service: TripsService,
    private drawerRef: NzDrawerRef<string>,
  ) { }

  ngOnInit() {
    this.getVehicles();
    this.getCities();
    this.form = this.fb.group({
      idTrip:[this.idTrip],
      name:['', [Validators.required]],
      description:['', [Validators.required]],
      cityId:[null, [Validators.required]],
      vehicleId:[null, [Validators.required]],
      tripDate:['', [Validators.required]] 
  },
  ); 
  
  }
  close(){
    this.drawerRef.close();
  }
  getVehicles(){
    this.service.getAllVehicles().subscribe((r) => {
      this.vehicles = r.data;
    });
  }
  getCities(){
    this.service.getAllCities().subscribe((r) => {
      this.cities = r.data;
    });
  }
  saveTrip(){
    this.isValidForm();
    if(this.form.valid){
      this.service.createOrUpdateTrip(this.form.getRawValue())
      .subscribe((r:any) => {
        this.close()
      })
    }
    
  }
  isValidForm(){
    if (this.form.controls['name'].invalid){
      this.errorName = true;
    }
    if (this.form.controls['description'].invalid){
      this.errorDescription = true;
    }
    if (this.form.controls['cityId'].invalid){
      this.errorCity = true;
    }
    if (this.form.controls['vehicleId'].invalid){
      this.errorVehicle = true;
    }
    if (this.form.controls['tripDate'].invalid){
      this.errorDate = true;
    }
  }

}
