import { Component, OnInit } from '@angular/core';
import { CitiesParams, CitiesResponse } from '../../trips/list/models/city.model';
import { environment } from 'src/environments/environment';
import { CitiesService } from '../cities.service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { AddCitiesComponent } from '../add-cities/add-cities.component';
import { BaseComponent } from 'src/app/common/base/base.component';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-list-cities',
  templateUrl: './list-cities.component.html',
  styleUrls: ['./list-cities.component.css']
})
export class ListCitiesComponent extends BaseComponent implements OnInit {
  
  params: CitiesParams = {
    name:'',
    country:'',
    page: 0,
    pageSize: environment.pageSize,
  }
  data: CitiesResponse[]=[]
  totalCount:number = 0;
  isTableLoading:false;
  constructor(
    private service: CitiesService,
    private drawerService :NzDrawerService,
    private modalService: NzModalService,
  ) {  super() }

  ngOnInit() {
    this.getCitiesList();
  }

  getCitiesList(){
    this.service.listCities(this.params).subscribe((r) => {
    this.data = r.data;
    this.totalCount = r.totalCount;
    })
  }
  addNewCity(data?:any){
    const drawerRef =
    this.drawerService.create<AddCitiesComponent, {data:any}>({
      nzTitle: "Cargar nueva ciudad",
      nzClosable: true,
      nzContent: AddCitiesComponent,
      nzPlacement:'right',
      nzContentParams:{data: data},
      nzWidth: '800px',
    })
    drawerRef.afterClose.subscribe((r) => {
        this.getCitiesList();
      })
  }
  showDeleteConfirmation(data: any) {
    this.modalService.confirm({
      nzTitle: '¿Eliminar ciudad?',
      nzContent: `Si confirmas esta acción, se eliminará la ciudad de forma permanente.`,
      nzOkText: 'Eliminar',
      nzOkDanger: true,
      nzOnOk: () => {
        this.deleteCity(data.id);
      },
      nzCancelText: 'Cancelar',
      nzClosable: false,
      nzCentered: true,
    });
  }
  deleteCity(id: number): void {
    this.service.deleteCity(id)
      .subscribe({
        next: () => {
          this.showMessageSuccess('La ciudad fue eliminada correctamente.');
          this.getCitiesList();
        },
        error: () => (this.isTableLoading = false)
      });
  }

}
