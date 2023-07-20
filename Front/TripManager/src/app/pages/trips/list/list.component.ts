import { Component, OnInit } from '@angular/core';
import { TripDto, TripParams } from './models/trip.model';
import { environment } from 'src/environments/environment';
import { TripsService } from '../trips.service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { AddTripComponent } from '../add-trip/add-trip.component';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzMessageService } from 'ng-zorro-antd/message';
import { BaseComponent } from 'src/app/common/base/base.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent extends BaseComponent implements OnInit {
  params: TripParams = {
    cityId: 0,
    vehicleId:0,
    tripDate:null,
    page:0,
    pageSize: environment.pageSize
  }
  data:TripDto[] = []
  totalCount: number = 0;
  isTableLoading:false;

  constructor(
    private service : TripsService,
    private drawerService: NzDrawerService,
    private modalService: NzModalService,
    message: NzMessageService,
    ) { super() }

  ngOnInit() {
    this.getTripsList();
  }

  getTripsList(){
    this.service.listTrips(this.params).subscribe((r) => {
    this.data = r.data;
    this.totalCount = r.totalCount;
    })
  }
  addNewTrip(data?:any){
    const drawerRef =
    this.drawerService.create<AddTripComponent, {data:any}>({
      nzTitle: "Cargar nuevo viaje",
      nzClosable: true,
      nzContent: AddTripComponent,
      nzPlacement:'right',
      nzContentParams:{data: data},
      nzWidth: '800px',
    })
    drawerRef.afterClose.subscribe((r) => {
        this.getTripsList();
      })
  }

  showDeleteConfirmation(data: any) {
    this.modalService.confirm({
      nzTitle: '¿Eliminar viaje?',
      nzContent: `Si confirmas esta acción, se eliminará el viaje de forma permanente.`,
      nzOkText: 'Eliminar',
      nzOkDanger: true,
      nzOnOk: () => {
        this.deleteTrip(data.id);
      },
      nzCancelText: 'Cancelar',
      nzClosable: false,
      nzCentered: true,
    });
  }
  deleteTrip(id: number): void {
    this.service.deleteTrip(id)
      .subscribe({
        next: () => {
          this.showMessageSuccess('El viaje fue eliminada correctamente.');
          this.getTripsList();
        },
        error: () => (this.isTableLoading = false)
      });
  }

}
