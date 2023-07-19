import { NgModule } from '@angular/core';
import { ListCitiesComponent } from './list-cities/list-cities.component';
import { RouterModule, Routes } from '@angular/router';
import { AddCitiesComponent } from './add-cities/add-cities.component';

const routes : Routes = [
  {
      path: 'list', component: ListCitiesComponent
  },
  {
    path:':id', component: AddCitiesComponent
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CitiesRoutingModule { }
