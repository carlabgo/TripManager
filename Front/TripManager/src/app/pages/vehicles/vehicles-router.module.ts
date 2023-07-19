import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list-vehicle/list-vehicle.component';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';

const routes : Routes = [
  {
      path: 'list', component: ListComponent
  },
  {
    path:':id', component: AddVehicleComponent
}
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class VehiclesRouterModule { }
