import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  private basePath = 'https://localhost:7046/api/v1/City';
constructor(private httpClient: HttpClient) { }

listCities(data: any):Observable<any>{
  return this.httpClient.post(
    `${this.basePath}/list`,
    data
    );
}
createOrUpdateCity(data:any):Observable<any>{
  return this.httpClient.post(`${this.basePath}/createOrUpdate`, data);
}
deleteCity(id:any): Observable<any>{
  return this.httpClient.delete(`${this.basePath}/${id}`);
}

}
