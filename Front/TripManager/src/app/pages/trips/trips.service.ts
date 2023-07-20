import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TripsService {
private basePath = 'https://localhost:7046/api/v1/';
private trip = 'https://localhost:7046/api/v1/Trip'
private vehicle = 'Vehicle'
private city = 'City'

constructor(private httpClient: HttpClient) { }

listTrips(data: any):Observable<any>{
  return this.httpClient.post(
    `${this.trip}/list`,
    data
    );
}
createOrUpdateTrip(data:any):Observable<any>{
  return this.httpClient.post(`${this.trip}/createOrUpdate`, data);
}
getAllVehicles():Observable<any>{
  return this.httpClient.get(`${this.basePath}${this.vehicle}/listAll`);
}
getAllCities():Observable<any>{
  return this.httpClient.get(`${this.basePath}${this.city}/listAll`);
}
deleteTrip(id:any): Observable<any>{
  return this.httpClient.delete(`${this.trip}/${id}`);
}
getWeatherForecast(date: string, cityName: string): Promise<any> {
  const apiKey = 'bf95850c68ea06df9ece21d2ceed495c';
  const apiUrl = `https://api.openweathermap.org/data/2.5/forecast?appid=${apiKey}&q=${cityName}&units=metric`;

  return this.httpClient.get(apiUrl).pipe(
    map((response: any) => {
      const forecasts = response.list.map((item: any) => ({
        date: new Date(item.dt_txt),
        rainy: item.weather.some((w: any) => w.main.toLowerCase() === 'rain'),
      }));
      const forecast = forecasts.find((f: any) => f.date.toDateString() === new Date(date).toDateString());
      return forecast;
    })
  ).toPromise();
}
// getWeatherForecast(date: string, cityName: string): Promise<any> {
//   return Promise.resolve({ date: new Date(date), rainy: true });
// }

}
