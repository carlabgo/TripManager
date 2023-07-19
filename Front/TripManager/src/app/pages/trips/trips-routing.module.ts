import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ListComponent } from "./list/list.component";
import { AddTripComponent } from "./add-trip/add-trip.component";


const routes : Routes = [
    {
        path: 'list', component: ListComponent
    },
    {
        path:':id', component: AddTripComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TripsRoutingModule { }