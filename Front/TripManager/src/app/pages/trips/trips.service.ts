import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TripsService {
private basePath = 'https://localhost:7046/api/v1/';
private trip = 'Trip'
private vehicle = 'Vehicle'
private city = 'City'

constructor(private httpClient: HttpClient) { }

listTrips(data: any):Observable<any>{
  return this.httpClient.post(
    `${this.basePath}${this.trip}/list`,
    data
    );
}
createOrUpdateTrip(data:any):Observable<any>{
  return this.httpClient.post(`${this.basePath}${this.trip}/createOrUpdate`, data);
}
getAllVehicles():Observable<any>{
  return this.httpClient.get(`${this.basePath}${this.vehicle}/list`);
}
getAllCities():Observable<any>{
  return this.httpClient.get(`${this.basePath}${this.city}/list`);
}

}
