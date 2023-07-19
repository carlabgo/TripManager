import { Component, OnInit } from '@angular/core';
import { TripDto, TripParams } from './models/trip.model';
import { environment } from 'src/environments/environment';
import { TripsService } from '../trips.service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { AddTripComponent } from '../add-trip/add-trip.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  params: TripParams = {
    cityId: 0,
    vehicleId:0,
    tripDate:null,
    page:0,
    pageSize: environment.pageSize
  }
  data:TripDto[] = []
  totalCount: number = 0;
  constructor(
    private service : TripsService,
    private drawerService: NzDrawerService
    ) { }

  ngOnInit() {
    this.getTripsList();
  }

  getTripsList(){
    console.log(this.params);
    this.service.listTrips(this.params).subscribe((r) => {
    this.data = r.data;
    this.totalCount = r.totalCount;
    })
  }
  addNewTrip(idTrip?:number){
    const drawerRef =
    this.drawerService.create<AddTripComponent, {idTrip:number}>({
      nzTitle: "Cargar nuevo viaje",
      nzClosable: true,
      nzContent: AddTripComponent,
      nzPlacement:'right',
      nzContentParams:{idTrip: idTrip},
      nzWidth: '800px',
    })
    drawerRef.afterClose.subscribe((r) => {
        this.getTripsList();
      })
  }

}
