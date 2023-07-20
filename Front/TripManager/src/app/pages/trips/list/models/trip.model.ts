export class TripParams {
    cityId:number;
    vehicleId:number;
    tripDate:Date;
    page:number;
    pageSize:number;
}
export class TripDto {
    id:number;
    name:string;
    description:string;
    vehicle:string;
    city:string;
    tripDate:Date;
}
export class TripResponse{
    id:number;
    name:string;
    description:string;
    vehicleId:number;
    cityId:number;
    tripDate:Date;
}