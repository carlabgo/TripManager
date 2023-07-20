import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CitiesService } from '../cities.service';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { CitiesResponse } from '../../trips/list/models/city.model';

@Component({
  selector: 'app-add-cities',
  templateUrl: './add-cities.component.html',
  styleUrls: ['./add-cities.component.css']
})
export class AddCitiesComponent implements OnInit {

  form!:FormGroup;
  errorName:boolean = false;
  errorCountry:boolean = false;
  isLoading=false;
  @Input() data:CitiesResponse;
  constructor(
    router:Router,
    private fb: FormBuilder,
    private service: CitiesService,
    private drawerRef: NzDrawerRef<string>,
  ) { }

  ngOnInit() {
    this.createForm();
    if(this.data){
      this.form.setValue({
        id: this.data.id,
        name: this.data.name,
        country:this.data.country
      });
    }
  }
  
  createForm(){
    this.form = this.fb.group({
      id:[0],
      name:['', [Validators.required]],
      country:['', [Validators.required]],
    });
  }
  saveCity(){
    this.isValidForm();
    if(this.form.valid){
      this.service.createOrUpdateCity(this.form.getRawValue())
      .subscribe((r:any) => {
        this.close()
      })
    }
    
  }
  isValidForm(){
    if (this.form.controls['name'].invalid){
      this.errorName = true;
    }
    if (this.form.controls['country'].invalid){
      this.errorCountry = true;
    }
  }
  close(){
    this.drawerRef.close();
  }

}
