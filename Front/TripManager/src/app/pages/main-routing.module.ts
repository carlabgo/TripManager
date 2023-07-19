import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";

const routes : Routes = [
    {
        path: '', component: HomeComponent,
        children: [
            {
                path:'trips',
                loadChildren: () => import('./trips/trips.module').then((m) => m.TripsModule)
            },
            {
                path:'vehicles',
                loadChildren: () => import('./vehicles/vehicles.module').then((m) => m.VehiclesModule)
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PagesRoutingModule { }