import { Component, OnInit } from '@angular/core';
import { VehicleParams, VehicleResponse } from '../../trips/list/models/vehicle.model';
import { VehiclesService } from '../vehicles.service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { environment } from 'src/environments/environment';
import { AddVehicleComponent } from '../add-vehicle/add-vehicle.component';
import { BaseComponent } from 'src/app/common/base/base.component';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-list',
  templateUrl: './list-vehicle.component.html',
  styleUrls: ['./list-vehicle.component.css']
})
export class ListComponent extends BaseComponent implements OnInit {
  params: VehicleParams = {
    brand:'',
    typeId:0,
    licensePlate:'',
    page:0,
    pageSize: environment.pageSize
  }
  data: VehicleResponse[]=[]
  totalCount:number = 0;
  isTableLoading:false;
  constructor(
    private service: VehiclesService,
    private drawerService: NzDrawerService,
    private modalService: NzModalService,
  ) { super() }

  ngOnInit() {
    this.getVehiclesList();
  }
  getVehiclesList(){
    this.service.listVehicles(this.params).subscribe((r) => {
    this.data = r.data;
    this.totalCount = r.totalCount;
    })
  }

  addNewVehicle(id?:number){
    const drawerRef =
    this.drawerService.create<AddVehicleComponent, {id:number}>({
      nzTitle: "Cargar nuevo vehiculo",
      nzClosable: true,
      nzContent: AddVehicleComponent,
      nzPlacement:'right',
      nzContentParams:{id: id},
      nzWidth: '800px',
    })
    drawerRef.afterClose.subscribe((r) => {
        this.getVehiclesList();
      })
  }
  showDeleteConfirmation(data: any) {
    this.modalService.confirm({
      nzTitle: '¿Eliminar vehiculo?',
      nzContent: `Si confirmas esta acción, se eliminará el vehiculo de forma permanente.`,
      nzOkText: 'Eliminar',
      nzOkDanger: true,
      nzOnOk: () => {
        this.deleteVehicle(data.id);
      },
      nzCancelText: 'Cancelar',
      nzClosable: false,
      nzCentered: true,
    });
  }
  deleteVehicle(id: number): void {
    this.service.deleteVehicle(id)
      .subscribe({
        next: () => {
          this.showMessageSuccess('El vehiculo fue eliminado correctamente.');
          this.getVehiclesList();
        },
        error: () => (this.isTableLoading = false)
      });
  }
}
