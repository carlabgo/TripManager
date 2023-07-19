import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list-vehicle/list-vehicle.component';

const routes : Routes = [
  {
      path: 'list', component: ListComponent
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class VehiclesRouterModule { }
