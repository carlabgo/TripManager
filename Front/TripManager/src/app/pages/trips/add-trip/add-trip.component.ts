import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TripsService } from '../trips.service';
import { NzDrawerRef} from 'ng-zorro-antd/drawer'
import { CityDTO } from '../list/models/city.model';
import { VehicleDTO } from '../list/models/vehicle.model';
import { NzModalService } from 'ng-zorro-antd/modal';


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
  selectedCityName: string;

  @Input() id:number;

  constructor(
    router:Router,
    private fb: FormBuilder,
    private service: TripsService,
    private drawerRef: NzDrawerRef<string>,
    private modalService: NzModalService
  ) { }

  ngOnInit() {
    this.getVehicles();
    this.getCities();
    this.form = this.fb.group({
      id:[0,this.id],
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
  showRainyModal(tripData: any) {
    this.modalService.confirm({
      nzTitle: 'Alerta de pronóstico de lluvia',
      nzContent: 'Se pronostica lluvia para la fecha y ciudad seleccionadas. ¿Deseas reprogramar el viaje?',
      nzOkText: 'Reprogramar',
      nzCancelText: 'Dejar fecha actual',
      nzOnOk: () => {
        this.form.patchValue({ tripDate: new Date() });
      },
      nzOnCancel: () => {
        this.service.createOrUpdateTrip(tripData).subscribe((response: any) => {
          this.close();
        });
      }
    });
  }
  async saveTrip() {
    this.isValidForm();
    if (this.form.valid) {
      const tripData = this.form.getRawValue();
      const selectedCity = this.cities.find(city => city.id === tripData.cityId);
      if (selectedCity) {
        try {
          const weatherForecast = await this.service.getWeatherForecast(tripData.tripDate, selectedCity.name);
          if (weatherForecast?.rainy) {
            this.showRainyModal(tripData);
          } else {
            this.service.createOrUpdateTrip(tripData).subscribe((r: any) => {
              this.close();
            });
          }
        } catch (error) {
          console.error('Error al obtener el pronóstico del clima:', error);
        }
      } else {
        console.error('Error: La ciudad seleccionada no se encuentra en la lista de ciudades.');
      }
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
