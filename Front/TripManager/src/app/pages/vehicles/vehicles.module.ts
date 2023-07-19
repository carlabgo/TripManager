import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { VehiclesService } from './vehicles.service';
import { VehiclesRouterModule } from './vehicles-router.module';
import { ListComponent } from './list-vehicle/list-vehicle.component';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzMessageModule } from 'ng-zorro-antd/message';


@NgModule({
imports: [
    CommonModule,
    VehiclesRouterModule,
    NzDividerModule,
    NzTableModule,
    NzDrawerModule,
    NzPageHeaderModule,
    NzButtonModule,
    NzIconModule,
    NzSelectModule,
    FormsModule,
    ReactiveFormsModule,
    NzFormModule,
    NzDatePickerModule,
    NzModalModule,
    NzMessageModule
],
declarations: [
    ListComponent,
    AddVehicleComponent
    
],
providers: [
    VehiclesService
]
})
export class VehiclesModule { }
