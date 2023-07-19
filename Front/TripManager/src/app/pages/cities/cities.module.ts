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
import { ListCitiesComponent } from './list-cities/list-cities.component';
import { CitiesService } from './cities.service';
import { CitiesRoutingModule } from './cities-routing.module';
import { AddCitiesComponent } from './add-cities/add-cities.component';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzMessageModule } from 'ng-zorro-antd/message';



@NgModule({
  imports: [
    CommonModule,
    CitiesRoutingModule,
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
    NzModalModule,
    NzMessageModule
  ],
  declarations: [
    ListCitiesComponent,
    AddCitiesComponent
  ],
  providers: [
    CitiesService
]
})
export class CitiesModule { }
