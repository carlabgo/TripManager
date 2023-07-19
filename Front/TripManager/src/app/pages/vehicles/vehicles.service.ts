import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehiclesService {
  private basePath = 'https://localhost:7046/api/v1/Vehicle';
constructor(private httpClient: HttpClient) { }

listVehicles(data: any):Observable<any>{
  return this.httpClient.post(
    `${this.basePath}/list`,
    data
    );
}
createOrUpdateVehicle(data:any):Observable<any>{
  return this.httpClient.post(`${this.basePath}/createOrUpdate`, data);
}
getTypes():Observable<any>{
  return this.httpClient.get(`${this.basePath}Type/list`);
}

}
