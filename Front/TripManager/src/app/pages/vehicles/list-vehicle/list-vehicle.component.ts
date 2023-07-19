import { Component, OnInit } from '@angular/core';
import { VehicleParams, VehicleResponse } from '../../trips/list/models/vehicle.model';
import { VehiclesService } from '../vehicles.service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { environment } from 'src/environments/environment';
import { AddVehicleComponent } from '../add-vehicle/add-vehicle.component';

@Component({
  selector: 'app-list',
  templateUrl: './list-vehicle.component.html',
  styleUrls: ['./list-vehicle.component.css']
})
export class ListComponent implements OnInit {
  params: VehicleParams = {
    brand:'',
    typeId:0,
    licensePlate:'',
    page:0,
    pageSize: environment.pageSize
  }
  data: VehicleResponse[]=[]
  totalCount:number = 0;
  constructor(
    private service: VehiclesService,
    private drawerService: NzDrawerService
  ) { }

  ngOnInit() {
    this.getVehiclesList();
  }
  getVehiclesList(){
    this.service.listVehicles(this.params).subscribe((r) => {
    this.data = r.data;
    this.totalCount = r.totalCount;
    })
  }

  addNewVehicle(idVehicle?:number){
    const drawerRef =
    this.drawerService.create<AddVehicleComponent, {idVehicle:number}>({
      nzTitle: "Cargar nuevo vehiculo",
      nzClosable: true,
      nzContent: AddVehicleComponent,
      nzPlacement:'right',
      nzContentParams:{idVehicle: idVehicle},
      nzWidth: '800px',
    })
    drawerRef.afterClose.subscribe((r) => {
        this.getVehiclesList();
      })
  }
}
