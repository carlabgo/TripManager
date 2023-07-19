export class CityDTO {
    id:number;
    name:string;
    }

export class CitiesParams {
    name:string;
    country:string;
    page:number;
    pageSize:number;
}
export class CitiesResponse {
    id:number;
    name:string;
    country:string;
}