import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { PagesRoutingModule } from "./main-routing.module";
import { HomeComponent } from "./home/home.component";
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzMenuModule } from "ng-zorro-antd/menu";
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzIconModule } from "ng-zorro-antd/icon";

@NgModule({
    imports: [
        CommonModule,
        PagesRoutingModule,
        NzLayoutModule,
        NzBreadCrumbModule,
        NzMenuModule,
        NzPageHeaderModule,
        NzIconModule
    ],
    declarations: [
        HomeComponent,
    ]
})
export class PagesModule {}