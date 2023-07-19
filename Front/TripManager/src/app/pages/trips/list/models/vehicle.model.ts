export class VehicleDTO {
id:number;
type:string;
}
export class VehicleParams {
    brand:string;
    typeId:0;
    licensePlate:string;
    page:number;
    pageSize:number;
}
export class VehicleResponse {
    id:number;
    brand:string;
    type:string;
    licensePlate:string;
}
export class VehicleType {
    id:number;
    name:string;
}