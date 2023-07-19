import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ListComponent } from "./list/list.component";
import { TripsRoutingModule } from "./trips-routing.module";
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzTableModule } from "ng-zorro-antd/table";
import { TripsService } from "./trips.service";
import { NzDrawerModule } from "ng-zorro-antd/drawer";
import { NzPageHeaderModule } from "ng-zorro-antd/page-header";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzSelectModule } from "ng-zorro-antd/select";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NzFormModule } from 'ng-zorro-antd/form';
import { AddTripComponent } from "./add-trip/add-trip.component";
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';

@NgModule({
    imports: [
        CommonModule,
        TripsRoutingModule,
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
        NzDatePickerModule
    ],
    declarations: [
        ListComponent,
        AddTripComponent
    ],
    providers: [
        TripsService
    ]
})
export class TripsModule {}